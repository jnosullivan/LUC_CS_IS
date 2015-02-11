using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.OpenGLES;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace BiDirectionalApp
{
	public class Foundation : UIView
	{

		 bool runnning = false;

		 bool completed = false;

		bool switcher = true;

		Problem problem;

		Graph graph;

		UIPanGestureRecognizer panGesture;

		int ViewTempTag;

		List<Edge> done;

		public Dictionary<String,int> ar = new Dictionary<String,int>(); 

		public Foundation ()
		{
			this.Frame = new RectangleF(0, 0, 500, 500);

			this.BackgroundColor = UIColor.DarkGray;
		}


		public void addNode(string value) {

			var newView = new Node();

			newView.SetTitleLabel(value);

			newView.Tag = ViewTempTag;

			ar.Add(value,ViewTempTag);

			ViewTempTag++;

			float dx = 0;

			float dy = 0;

			panGesture = new UIPanGestureRecognizer ((pg) => {

				if ((pg.State == UIGestureRecognizerState.Began || pg.State == UIGestureRecognizerState.Changed) && (pg.NumberOfTouches == 1)) {

					if(!runnning) {

					var p0 = pg.LocationInView (this);

					if (dx == 0)
						dx = p0.X - newView.Center.X;

					if (dy == 0)

						dy = p0.Y - newView.Center.Y;

					var p1 = new PointF (p0.X - dx, p0.Y - dy);

					newView.Center = p1;

					}

				} else if (pg.State == UIGestureRecognizerState.Ended) {

					dx = 0;

					dy = 0;

				}
			});

			newView.AddGestureRecognizer (panGesture);

			this.Add (newView);
		}




		public override void TouchesBegan (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			/*
			base.TouchesBegan (touches, evt);

			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) {
			}
			*/
		}

		public override void TouchesMoved (MonoTouch.Foundation.NSSet touches, UIEvent evt)
		{
			/*
			base.TouchesMoved (touches, evt);

			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) {

				SetNeedsDisplay ();

			}*/
		}

		/* static public void graphRedraw(Problem p) { } */

		PointF[] getPostion(int tagone, int tagtwo) {
			//Sets the blank parameters
			float tagonex = 0;
			float tagoney = 0;
			float tagtwox = 0;
			float tagtwoy = 0;

			foreach (var selectedview in this.Subviews)
			{
				if(selectedview.GetType() == typeof(Node)) {

					if(tagone == selectedview.Tag) {

						tagonex = selectedview.Frame.X + 25;

						tagoney = selectedview.Frame.Y + 25;

					}
					if(tagtwo == selectedview.Tag) {

						tagtwox = selectedview.Frame.X+ 25;

						tagtwoy = selectedview.Frame.Y+ 25;

					}

				}
			}
			return new PointF[]{ new PointF (tagonex, tagoney), new PointF (tagtwox, tagtwoy) };
		}


		public void drawDone(List<Edge> way) {


			done = way;

			completed = true;

			SetNeedsDisplay();

		}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);




			if (runnning) {
				/*
				foreach (KeyValuePair<String, int> pair in ar)
				{
					Console.WriteLine("{0}, {1}",
						pair.Key,
						pair.Value);
				}
				*/

				for (int i = 0; i < problem.forwards.Count; i++) {

					for (int j = 0; j < problem.forwards.ElementAt (i).Count; j++) {
					
						int id1 = ar[problem.forwards.ElementAt (i) [j].lA];

						int id2 = ar[problem.forwards.ElementAt (i) [j].lB];

						using(CGContext g = UIGraphics.GetCurrentContext ()){

							g.SetLineWidth (2);

							CGPath path;

							path = new CGPath();

							path.AddLines (getPostion(id1,id2));

							g.SetStrokeColor(UIColor.White.CGColor);

							g.AddPath (path);		

							g.DrawPath (CGPathDrawingMode.Stroke);

						}
					}
				}
				for (int i = 0; i < problem.backwards.Count; i++) {

					for (int j = 0; j < problem.backwards.ElementAt (i).Count; j++) {

						int id1 = ar[problem.backwards.ElementAt (i) [j].lA];

						int id2 = ar[problem.backwards.ElementAt (i) [j].lB];

						using(CGContext g = UIGraphics.GetCurrentContext ()){

							g.SetLineWidth (2);

							CGPath path;

							path = new CGPath();

							path.AddLines (getPostion(id1,id2));

							g.SetStrokeColor(UIColor.White.CGColor);

							g.AddPath (path);

							g.DrawPath (CGPathDrawingMode.Stroke);

						}
					}
				}

				for (int i = 0; i < problem.checkedforwards.Count; i++) {
					//Console.WriteLine ("Visited " + problem.checked1.ElementAt (i));
				}
				for (int i = 0; i < problem.checkedbackwards.Count; i++) {
					//Console.WriteLine ("Visited " + problem.checked1.ElementAt (i));

				}
				for (int i = 0; i < problem.backwardarray ().Count; i++) {
					//Console.WriteLine ("Checking " + problem.checked1.ElementAt (i));
				}
				for (int i = 0; i < problem.forwardarray ().Count; i++) {
					//Console.WriteLine ("Checking " + problem.checked1.ElementAt (i));
				}
				if (problem.isSolved ()) {
					//Console.WriteLine ("Results " + problem.result);
				}

			}

			if (completed) {
				Console.WriteLine("done show result");
				for (int i = 1; i < done.Count-1; i++)
				{
					int id1 = ar[done[i].lA.ToString()];

					int id2 = ar[done[i].lB.ToString()];

					using(CGContext g = UIGraphics.GetCurrentContext ()){

						g.SetLineWidth (2);

						CGPath path;

						path = new CGPath();

						path.AddLines (getPostion(id1,id2));

						g.SetStrokeColor(UIColor.Red.CGColor);

						g.AddPath (path);	

						g.DrawPath (CGPathDrawingMode.Stroke);

					}
				}
			}
				
		}	 
			
		public void setProblem(Problem p)
		{
			problem = p;
		}
		public Problem Run(Graph g)
		{
			graph = g;

			if(!problem.isSolved())
			{
				if (switcher)
				{
					KeyValuePair<Queue<List<Edge>>, HashSet<string>> kv = anaylzeStep(problem.forwards, problem.checkedforwards);

					problem.forwards = kv.Key;

					problem.checkedforwards = kv.Value;

					switcher = false;
				}
				else
				{
					KeyValuePair<Queue<List<Edge>>, HashSet<string>> kv = anaylzeStep(problem.backwards, problem.checkedbackwards);

					problem.backwards = kv.Key;

					problem.checkedbackwards = kv.Value;

					switcher = true;
				}
				runnning = true;

				SetNeedsDisplay();
			}
			return problem;
		}
		public KeyValuePair<Queue<List<Edge>>, HashSet<string>> anaylzeStep(Queue<List<Edge>> front, HashSet<string> h)
		{
			Queue<List<Edge>> newFront = new Queue<List<Edge>>();

			while (front.Count > 0)
			{
				List<Edge> oldWay = front.Dequeue();

				string p = oldWay.Last().lB;

				h.Add(p);

				foreach (string n in graph.Neighbors(p))
				{
					if (!h.Contains(n))
					{
						List<Edge> newWay = new List<Edge>();

						newWay.InsertRange(0,oldWay);

						newWay.Add(new Edge(oldWay.Last().lB, n));

						newFront.Enqueue(newWay);

					}
					h.Add(n);
				}
			}
			return new KeyValuePair<Queue<List<Edge>>, HashSet<string>>(newFront, h);
		}
	

	}
}

