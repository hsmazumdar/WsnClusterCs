using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections;
/*
Start
nodsz = Get number of nodes
grpssz = Get number of clusters
grpsz[] = Get node size of each clusters (temp only)
dist[][] = Get distance between all nodes
i = 0;
best = a big number;
loop1:
	chn = Get furthest node as cluster head node ch_i
	Mark grpsz[i] nearest cluster members state as -1
	Choos new cluster head in center of gravity of i_th cluster as ch_i
	i = i + 1
	if (i < grpssz) 
	 Goto loop1
goodness = sum of distance between all cluster-heads to members
Include nearest members of other groups to each ch by excluding from present group
if(best > goodness)
	Goto loop1  
Update grpsz[]
Result: Mark all cluster heads with cluster members	 
End
 */
namespace WsnHsmUtils
{
    public partial class WsnUtil : Form
    {
        //***********************************************************
        public struct NODES
        {
            public int id;
            public PointF pos;
            public PointF posSig;
            public float rcvPow;
            public float powInRate;
            public float powOutRate;
            public float powInSum;
            public float baseDist;
            public int bound;
            public int status;
            public float siz;
            public Color col;
            public ArrayList rxLog;
            public int grpId;
            public string[] signl;
        };
        struct SINK
        {
            public PointF pos;
            public int nodes;
            public int status;
            public float siz;
            public Color col;
            public ArrayList txrxLog;
            public string[] nodeAns;
        };
        struct CLUST
        {
            public int[] nods;
            public int nodCnt;
        };
        //***********************************************************
        public NODES[] nodes;
        NODES[] nodesBak;
        float best = 0;
        SINK sink;
        float offset = 0.025f;
        //ArrayList[] sigLst;
        Bitmap bmMain;
        Bitmap bmBack;
        float d30 = 20;
        float th50 = 0.5f;
        int[] grpSz;
        Brush brs0 = Brushes.Green;
        float grpW;
        float grpH;
        int colNo = 255;
        int ud = 0;
        int tm2Mode = 0;
        int secCount = 0;
        int maxCount = 1;
        bool doAll = false;
        Random rnd = new Random(DateTime.Now.Millisecond);
        ArrayList al;
        float[][] rxSignl;
        //int[] sigMin;
        int nds = -1;
        //***********************************************************
        public WsnUtil()
        {
            InitializeComponent();
            nds = -1;
        }
        //***********************************************************
        private void WsnUtil_Shown(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
            ClearScreen();
            CreateAdvancedNodes();
        }
        //***********************************************************
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //***********************************************************
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearScreen();
            File.Delete("PolyList.txt");
        }
        //**********************************************************
        private void ClearScreen()
        {
            bmMain = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmMain);
            g.Clear(Color.Blue);
            g.Dispose();
            pictureBox1.Image = bmMain;
        }
        //***********************************************************
        private void btnNodes_Click(object sender, EventArgs e)
        {
            al = new ArrayList();
            CreateAdvancedNodes();
        }
        //***********************************************************
        private void CreateAdvancedNodes()
        {
            float shrink = 1 - 2 * offset;
            bmMain = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            int mxNods = int.Parse(textBox1.Text);
            int grpMx = (int)Math.Round((Math.Sqrt(mxNods) + 0.5f));
            txbxGroups.Text = grpMx.ToString();
            nodes = new NODES[mxNods];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].siz = 1.0f;
                nodes[i].rxLog = new ArrayList();
                nodes[i].powInRate = 100;
                nodes[i].powInSum = 0;
                nodes[i].powOutRate = 0;
                nodes[i].status = 0;
                nodes[i].grpId = -1;
                nodes[i].col = Color.FromArgb(255, 255, 255);
                nodes[i].signl = new string[nodes.Length - 1];
            }
            int zoneX = int.Parse(txbxZones.Text.Split(',')[0]);
            int zoneY = int.Parse(txbxZones.Text.Split(',')[1]);
            float nodsGrp = (float)mxNods / ((zoneX * zoneY) - 1);//leave one for base stn.
            grpW = (float)(bmMain.Width * shrink) / zoneX;
            grpH = (float)(bmMain.Height * shrink) / zoneY;
            float xo = bmMain.Width * offset;
            float yo = bmMain.Height * offset;
            grpSz = new int[zoneX * zoneY];
            PointF[] grpOrg = new PointF[zoneX * zoneY];

            int n1 = 0;
            int n2 = rnd.Next(zoneX * zoneY);//location of base stn.
            float sum1 = 0;
            ArrayList al = new ArrayList();
            int nn = 0;
            int nnOld = 0;
            for (int y = 0; y < zoneY; y++)
            {
                float w = 0;
                for (int x = 0; x < zoneX; x++)
                {
                    if (n1 != n2)
                    {
                        sum1 += nodsGrp;
                        nn = (int)(sum1 + 0.5f);
                    }
                    al.Add(nn);
                    grpSz[n1] = nn - nnOld;
                    n1++;
                    nnOld = nn;
                }
            }
            int rn = int.Parse(txbxRand.Text);
            for (int i = 0; i < mxNods * rn; i++)
            {
                int n4 = rnd.Next(grpSz.Length);
                int n3 = rnd.Next(grpSz.Length);
                if ((grpSz[n4] > 1) & (grpSz[n3] > 1))
                {
                    grpSz[n4]++;
                    grpSz[n3]--;
                }
            }
            int xn = n2 % zoneX;
            int yn = n2 / zoneX;
            float xb = (float)(xn * grpW + rnd.NextDouble() * grpW);
            float yb = (float)(yn * grpH + rnd.NextDouble() * grpH);
            sink = new SINK();
            sink.nodeAns = new string[nodes.Length];
            sink.pos = new PointF(xb + xo, yb + yo);

            int n = 0;
            float sum = 0;
            float h = 0;
            int sno = 0;
            ArrayList al2 = new ArrayList();
            for (int y = 0; y < zoneY; y++)
            {
                float w = 0;
                for (int x = 0; x < zoneX; x++)
                {
                    sum += nodsGrp;
                    n++;
                    if (sno != n2)
                        sum -= (int)sum % n;
                    grpOrg[n - 1] = new PointF(w, h);
                    w += grpW;
                    for (int i = 0; i < grpSz[n - 1]; i++)
                    {
                        float xp = (float)(grpOrg[n - 1].X + rnd.NextDouble() * grpW);
                        float yp = (float)(grpOrg[n - 1].Y + rnd.NextDouble() * grpH);
                        nodes[sno].pos = new PointF(xp + xo, yp + yo);
                        nodes[sno].baseDist = (float)Math.Round(Distance(nodes[sno].pos, sink.pos), 2);
                        int xi = (int)xp;
                        int yi = (int)yp;
                        al2.Add(xi.ToString().PadLeft(4, '0') + "," + yi.ToString().PadLeft(4, '0') + "," + sno.ToString());
                        sno++;
                    }
                }
                h += grpH;
            }
            al2.Sort();
            for (int i = 0; i < al2.Count; i++)
            {
                nodes[int.Parse(al2[i].ToString().Split(',')[2])].id = i;
            }
            DrawNodes(true);
        }
        //***********************************************************
        private float Distance(PointF pt1, PointF pt2)
        {
            return (float)Math.Sqrt((pt1.X - pt2.X) * (pt1.X - pt2.X) + (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y));
        }
        //***********************************************************
        private void DrawNodes(bool clr)
        {
            if (nodes.Length == 0)
                return;
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Red);
            SolidBrush drawBrush2 = new SolidBrush(Color.Black);
            int zoneX = int.Parse(txbxZones.Text.Split(',')[0]);
            int zoneY = int.Parse(txbxZones.Text.Split(',')[1]);
            //float grpW = (float)pictureBox1.Width / zoneX;
            //float grpH = (float)pictureBox1.Height / zoneY;
            if (!clr)
                bmMain = (Bitmap)pictureBox1.Image;
            Graphics g = Graphics.FromImage(bmMain);
            if (clr)
                g.Clear(Color.Blue);
            float nd3 = 1 + d30 * nodes[0].siz;
            float nd4 = nd3 / 2;
            g.FillEllipse(Brushes.Red, sink.pos.X - nd4 * 2, sink.pos.Y - nd4 * 2, nd3 * 2, nd3 * 2);
            g.DrawString("BASE", drawFont, drawBrush2, sink.pos.X - nd4 * 2, sink.pos.Y - nd4);
            for (int n = 0; n < nodes.Length; n++)
            {
                SolidBrush brush = new SolidBrush(nodes[n].col);
                float nd = 1 + d30 * nodes[n].siz;
                float nd2 = nd / 2;

                if (nodes[n].siz < th50)
                    g.FillEllipse(Brushes.Red, nodes[n].pos.X - nd2, nodes[n].pos.Y - nd2, nd, nd);
                else
                    g.FillEllipse(brush, nodes[n].pos.X - nd2, nodes[n].pos.Y - nd2, nd, nd);
                //string num = Int2StringAA(nodes[n].id);
                string num = Int2StringAA(n);
                g.DrawString(num, drawFont, drawBrush, nodes[n].pos.X - nd2, nodes[n].pos.Y - nd2 / 2);
            }
            g.Dispose();
            pictureBox1.Image = bmMain;
        }
        //***********************************************************
        private string Int2StringAA(int ni)
        {
            char[] tbl = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M',
                                  'N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            int n1 = ni / 26;
            int n2 = ni % 26;
            //return tbl[n1].ToString() + tbl[n2].ToString();
            return ni.ToString();
        }
        //***********************************************************
        private void btnReDraw_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].siz = 1.0f;
            }
            DrawNodes(true);
        }
        //***********************************************************
        private void CreateRxSignal()
        {
            float dstMx = Distance(new PointF(0, 0), new PointF(bmMain.Width, bmMain.Height));
            rxSignl = new float[nodes.Length][];
            for (int i = 0; i < rxSignl.Length; i++)
            {
                rxSignl[i] = new float[rxSignl.Length];
                for (int j = 0; j < rxSignl[i].Length; j++)
                {
                    float dst = Distance(nodes[i].pos, nodes[j].pos);
                    if (dst != 0)
                    {
                        int sg = (int)(100 * dstMx * dstMx / (dst * dst));
                        rxSignl[i][j] = sg;
                    }
                    else
                    {
                        rxSignl[i][j] = 100 * dstMx * dstMx;
                    }
                }
            }
        }
        //***********************************************************
        private void btnAutoGroup_Click(object sender, EventArgs e)
        {
            AutoGroup();
        }
        //***********************************************************
        private void AutoGroup()
        {
            CreateRxSignal();
            best = float.MaxValue;
            ArrayList al = new ArrayList();
            int i = 0;
            int imx = 10;
            for (; ; )
            {
                CreateDrawCluster();
                float good = GetGoodness();
                if (best > good)
                {
                    if (i > imx / 2)
                        imx *= 2;
                    al.Add(good);
                    int gd = (int)good;
                    best = good;
                    SaveCluster();
                    pictureBox1.Refresh();
                    best = (int)best;
                    lblState.Text = best.ToString() + "," + al.Count.ToString() + "," + imx.ToString();
                    lblState.Refresh();
                }

                i++;
                if (i >= imx)
                    break;
            }
            RecalCluster();
            DrawNodes(true);
            DrawReCluster();
            DrawNodes(false);
            MarkCH();
            // btnStart.Focus();
        }
        //***********************************************************
        private void CreateDrawCluster()
        {
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i].siz = 1.0f;
                nodes[i].grpId = -1;
            }
            float chdIdeal = (float)Math.Sqrt(nodes.Length);
            chdIdeal = int.Parse(txbxGroups.Text);
            int[] nodGrp = new int[(int)(chdIdeal + 0.5)];
            float stp = (float)nodes.Length / nodGrp.Length;
            float sum = 0;
            for (int i = 0; i < nodGrp.Length; i++)
            {
                sum += stp;
                nodGrp[i] = (int)(sum + 0.5);
                sum -= nodGrp[i];
            }

            DrawNodes(true);
            CLUST clst = new CLUST();
            int nodFar0 = rnd.Next(nodes.Length);
            int nodFar1 = GetFurthestNode(nodFar0);
            int nodFar2 = GetFurthestNode(nodFar1);
            int nodFar3 = GetFurthestNode(nodFar2);
            clst.nodCnt = GetFurthestNode(nodFar3);
            // clst.nodCnt = nodes.Length / 2;//temp to be removed
            for (int i = 0; i < nodGrp.Length; i++)
            {
                int nodFar = GetFurthestNode(clst.nodCnt);
                clst = FormCluster(nodFar, nodGrp[i]);
                DrawCluster(clst);
            }
            LinkNearestCH();
            DrawNodes(true);
            DrawReCluster();
            DrawNodes(false);


            MarkCH();
        }
        //***********************************************************
        private float GetGoodness()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].grpId == i)
                    al.Add(i);
            }
            float sum = 0;
            for (int i = 0; i < al.Count; i++)
            {
                int clhd = (int)al[i];
                for (int j = 0; j < nodes.Length; j++)
                {
                    if (nodes[j].grpId == clhd)
                    {
                        //sum += Distance(nodes[clhd].pos, nodes[j].pos);
                        sum += DistanceV(clhd, j); //New Added
                    }
                }
            }
            return sum;
        }
        //***********************************************************
        private void SaveCluster()
        {
            nodesBak = new NODES[nodes.Length];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodesBak[i] = nodes[i];
            }
        }
        //***********************************************************
        private void RecalCluster()
        {
            nodes = new NODES[nodesBak.Length];
            for (int i = 0; i < nodesBak.Length; i++)
            {
                nodes[i] = nodesBak[i];
            }
        }
        //***********************************************************
        private void DrawReCluster()
        {
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen greenPen = new Pen(Color.FromArgb(255, 0, 255, 0), 4);
            Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0), 3);
            Bitmap bm = (Bitmap)bmMain.Clone();
            //Bitmap bm = (Bitmap)pictureBox1.Image;
            Graphics g = Graphics.FromImage(bm);
            ArrayList al = new ArrayList();
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].grpId == i)
                {
                    PointF pt = nodes[i].pos;
                    for (int j = 0; j < nodes.Length; j++)
                    {
                        if (nodes[j].grpId == i)
                            g.DrawLine(greenPen, pt, nodes[j].pos);
                    }
                }
            }
            g.Dispose();
            pictureBox1.Image = bm;
        }
        //***********************************************************
        private int GetFurthestNode(int nodIn)
        {
            float dstMx = float.MinValue;
            int nodFr = -1;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].grpId == -1)
                {
                    //float dst = Distance(nodes[nodIn].pos, nodes[i].pos);
                    float dst = DistanceV(nodIn, i); //New Added
                    if (dstMx < dst)
                    {
                        dstMx = dst;
                        nodFr = i;
                    }
                }
            }
            return nodFr;
        }
        //***********************************************************
        private CLUST FormCluster(int nod, int grpSz)
        {
            CLUST clst = new CLUST();
            float chdIdeal = (float)Math.Sqrt(nodes.Length);
            int nodCg = nod;
            nodes[nodCg].grpId = nodCg;
            ArrayList al1 = new ArrayList();
            al1.Add(nodCg);
            for (; ; )
            {
                int nodNr = GetNearestNode(nodCg);
                if (nodNr >= 0)
                {
                    al1.Add(nodNr);
                    nodes[nodNr].grpId = nod;
                }
                else
                {
                    break;
                }
                //nodCg = GetNodeCG(al1);
                nodCg = GetSigNodeCG(al1); // New Added
                if (al1.Count >= grpSz)
                    break;
            }
            clst.nods = new int[al1.Count];
            clst.nodCnt = nodCg;
            int m = 0;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].grpId == nod)
                {
                    clst.nods[m++] = i;
                    nodes[i].grpId = nodCg;
                }
            }
            return clst;
        }
        //***********************************************************
        private void DrawCluster(CLUST clst)
        {
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Pen greenPen = new Pen(Color.FromArgb(255, 0, 255, 0), 6);
            Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0), 3);
            //Bitmap bm = (Bitmap)bmMain.Clone();
            Bitmap bm = (Bitmap)pictureBox1.Image;
            Graphics g = Graphics.FromImage(bm);
            //int clhd = 0;
            PointF pt = nodes[clst.nodCnt].pos;
            for (int i = 0; i < clst.nods.Length; i++)//
            {
                g.DrawLine(greenPen, pt, nodes[clst.nods[i]].pos);
            }
            for (int i = 0; i < clst.nods.Length; i++)
            {
                string num = Int2StringAA(clst.nods[i]);
                float nd = 1 + d30 * nodes[clst.nods[i]].siz;
                float nd2 = nd / 2;
                g.DrawString(num, drawFont, drawBrush, nodes[clst.nods[i]].pos.X - nd2, nodes[clst.nods[i]].pos.Y - nd2 / 2);
            }
            g.Dispose();
            pictureBox1.Image = bm;
        }
        //***********************************************************
        private void LinkNearestCH()
        {
            ArrayList al = new ArrayList();
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].grpId == i)
                {
                    al.Add(i);
                }
            }
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].grpId != i)
                {
                    int jMn = -1;
                    float dstMn = float.MaxValue;
                    for (int j = 0; j < al.Count; j++)
                    {
                        //float dst = Distance(nodes[i].pos, nodes[(int)al[j]].pos);
                        float dst = DistanceV(i, (int)al[j]); //New Added
                        if (dstMn > dst)
                        {
                            dstMn = dst;
                            jMn = (int)al[j];
                        }
                    }
                    if (nodes[i].grpId != nodes[jMn].grpId)
                    {
                        DrawLine(nodes[i].pos, nodes[jMn].pos, Color.Red);
                        nodes[i].grpId = jMn;
                    }
                }
            }
        }
        //***********************************************************
        private int GetNearestNode(int nod)
        {
            float dstMn = float.MaxValue;
            int nodNr = -1;
            for (int i = 0; i < nodes.Length; i++)
            {
                if (i != nod)
                {
                    if (nodes[i].grpId == -1)
                    {
                        //float dst = Distance(nodes[i].pos, nodes[nod].pos);
                        float dst = DistanceV(i, nod); //New Added
                        if (dstMn > dst)
                        {
                            dstMn = dst;
                            nodNr = i;
                        }
                    }
                }
            }
            return nodNr;
        }
        //***********************************************************
        private int GetSigNodeCG(ArrayList alNod) // New Added
        {
            ArrayList dis = new ArrayList();
            if (alNod.Count > 1)
            {
                for (int i = 0; i < alNod.Count; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < alNod.Count; j++)
                    {
                        if (alNod[j] != alNod[i])
                            sum += (int)DistanceV((int)alNod[j], (int)alNod[i]);
                    }
                    dis.Add(sum.ToString().PadLeft(7,'0') + "," + alNod[i].ToString());
                }
                dis.Sort();
                string[] wrds = dis[0].ToString().Split(',');
                int no = int.Parse(wrds[1]);
                return no;
            }
            else
            {
                return (int)alNod[0];
            }       
        }
        //***********************************************************
        private void DrawLine(PointF pt1, PointF pt2, Color col)
        {
            Pen redPen = new Pen(col, 2);
            Bitmap bm = (Bitmap)pictureBox1.Image;
            Graphics g = Graphics.FromImage(bm);
            g.DrawLine(redPen, pt1, pt2);
            g.Dispose();
            pictureBox1.Image = bm;
        }
        //***********************************************************
        private void MarkCH()
        {
            Bitmap bm = (Bitmap)pictureBox1.Image;
            Graphics g = Graphics.FromImage(bm);
            Pen redPen = new Pen(Color.FromArgb(255, 255, 0, 0), 2);
            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].grpId == i)
                {
                    PointF pt = nodes[i].pos;
                    g.DrawEllipse(redPen, new RectangleF(pt.X - 15, pt.Y - 15, 30, 30));
                }
            }
            g.Dispose();
            pictureBox1.Image = bm;
        }
        //***********************************************************
        private float DistanceV(int pt1, int pt2) //New Added
        {
            float dstMx = Distance(new PointF(0, 0), new PointF(bmMain.Width, bmMain.Height));
            return (float)Math.Sqrt(100 * dstMx * dstMx / rxSignl[pt1][pt2]);

        }
        //***********************************************************
    }
}