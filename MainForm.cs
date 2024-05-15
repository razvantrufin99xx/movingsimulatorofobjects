/*
 * Created by SharpDevelop.
 * User: razvan
 * Date: 5/15/2024
 * Time: 2:54 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ball
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		public Graphics g;
		public Pen p = new Pen(Color.FromArgb(5,225,150,20),30);
		public Pen p2 = new Pen(Color.FromArgb(5,175,25,50),30);
		public Pen p3 = new Pen(Color.FromArgb(5,140,50,20),30);
		public Pen p4 = new Pen(Color.FromArgb(5,150,10,25),30);
		public Pen p5 = new Pen(Color.FromArgb(5,155,20,250),30);
		public Brush b = new SolidBrush(Color.Blue);
		public Font f;
		
		public class game
		{
			public Random rtn = new Random();
			public teren tr = new teren();
			public List<ballcls> bc = new List<ballcls>();
			public void addNBalls(int n)
			{
				for(int i = 0 ; i < n ; i++)
				{
					addBall(new ballcls(i+rtn.Next(2,205),i+rtn.Next(5,200),1,1));
					bc[i].dirx = rtn.Next(3,5);
					bc[i].diry = rtn.Next(1,3);
				}
			}
			public void addBall(ballcls bll)
			{
				bc.Add(bll);
			}
			public bool collision(ballcls tb, ref teren t)
			{
				if(tb.x < t.x){
					
					tb.dirx *=-1 ;
					
					return true;
				}
				if(tb.x > t.w){
					
					tb.dirx *=-1 ;
					
					return true;
				}
				if(tb.y < t.y){
					
					tb.diry *=-1 ;
					
					return true;
				}
				if(tb.y > t.h){
					
					tb.diry *=-1 ;
					
					return true;
				}
				return false;
			}
			public void testAllCollisions(List<ballcls> rb, ref teren ter)
			{
				for(int i = 0 ; i < rb.Count ; i++)
				{
					if(collision(rb[i], ref ter)==true)
					{
						//Console.Beep(32000,5);
					}
				}
			}
			public void moveAllBalls(ref List<ballcls> rb)
			{
				for(int i = 0 ; i < rb.Count ; i++)
				{
					bc[i].moveBall();
				}
			}
			public void drawAllBalls(ref List<ballcls> rb, ref Graphics ppg,  Pen ppp)
			{
				for(int i = 0 ; i < rb.Count ; i++)
				{
					bc[i].drawBall(ref ppg, ref ppp);
				}
			}
		}
		public class teren
		{
			public int x = 10;
			public int y = 10;
			public int w = 800;
			public int h = 600;
			
			public void drawTeren(ref Graphics pg,ref Pen pp )
			{
				pg.DrawRectangle(pp,x,y,w,h);
			}
		}
		public class ballcls
		{
			public int x = 100;
			public int y = 200;
			public int dirx = -20;
			public int diry = 20;
			public ballcls(int a, int b,int dx, int dy)
			{
				x = a;
				y = b;
				dirx = dx;
				diry = dy;
			}
			public void moveBall()
			{
				this.x += dirx;
				this.y += diry;
			}
			
			public void drawBall(ref Graphics pg,ref Pen pp )
			{
				pg.DrawEllipse(pp,x,y,20,20);
			}
		}
		public game thegame = new game();
		public int counter = 0;
		public List<Pen> cl = new List<Pen>();
		public Random rdn = new Random();
		void MainFormLoad(object sender, EventArgs e)
		{
			this.Left = 0;
			this.Top = 0;
			this.Width = 1920;
			this.Height = 1080;
			g = this.panel1.CreateGraphics();
			f = this.Font;
			thegame.addNBalls(25);
			timer1.Enabled = true;
			timer1.Interval = 1;
			cl.Add(p);
			cl.Add(p2);
			cl.Add(p3);
			cl.Add(p4);
			cl.Add(p5);
			
		}
		void Timer1Tick(object sender, EventArgs e)
		{
			//counter+=1;
			//Text = counter.ToString();
			//bool a = false;
			
			thegame.testAllCollisions(thegame.bc, ref thegame.tr);
			
			/*
			if (counter % 100 == 0)
			{
				g.Clear(Color.Black);
				//Console.Beep(32000,10);
			}
			*/
			thegame.moveAllBalls(ref thegame.bc);
			thegame.tr.drawTeren(ref this.g, ref this.p);
			thegame.drawAllBalls(ref thegame.bc, ref this.g, cl[rdn.Next(0,4)]);
			//thegame.drawAllBalls(ref thegame.bc, ref this.g, cl[rdn.Next(1)]);
		}
	}
}
