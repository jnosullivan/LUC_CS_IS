using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BiDirectionalApp
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		//protected Graph graph;
		//Problem task;
		//Algorism algo;
		//public bool isdone = false;

		public string nodeA = "";
		public string nodeB = "";


		public List<String> dataItems = new List<String>();


		public override UIWindow Window {
			get;
			set;
		}

		public bool Compare(string str) {



			if (nodeA == "" && nodeB == "") {
				nodeA = str;
				Console.WriteLine("NODEA: " + nodeA);
				Console.WriteLine("NODEA: " + nodeB);
				return false;
			}

			if (nodeA == str) {
				nodeA = str;
				Console.WriteLine("NODEA: " + nodeA);
				Console.WriteLine("NODEA: " + nodeB);
				return false;
			} else {
				nodeB = str;
				Console.WriteLine("NODEA: " + nodeA);
				Console.WriteLine("NODEA: " + nodeB);
				return true;
			}



			return false;
		}

	

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			// Override point for customization after application launch.
			Console.WriteLine("Starting");




			/*
			algo = new Algorism();
			task = new Problem();


			var documents =
				Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);
			var filename = Path.Combine (documents, "test.txt");
			File.WriteAllText(filename, "a b 151\na c 210\na d 134\na e 136\na f 270\nb d 163\nb c 74\nc d 156\nc i 190\nd e 150\nd g 174\nd h 115\ne g 136\ng h 189\nh i 185\nh j 121\ni k 142\nj q 319\nj r 429\nj k 266\nk l 142\nk m 336\nk n 190\nk o 475\nm p 178\nm s 189\nn p 251\nn q 128\no r 135\np s 148\nq t 239\nq r 185\nr w 86\ns t 224\ns y 309\ns x 337\nt y 258\nt u 90\nu z 373\nu w 291\nw z 278\nx y 148");


			//var text = System.IO.File.ReadAllText("TestData/test.txt");
			setGraph ("");
			setProblem ("a","z");


			while (!isdone) {

				Run ();
			}
*/


			Console.WriteLine("Done with working!");
			return true;
		}
		/*
		public void setProblem(string a, string b)
		{
			task.point1 = a;
			task.point2 = b;
			task.ways1 = new Queue<List<Edge>>();
			task.ways2 = new Queue<List<Edge>>();
			Edge edge1 = new Edge(task.point1,task.point1);
			List<Edge> list1 = new List<Edge>();
			list1.Add(edge1);
			task.ways1.Enqueue(list1);
			Edge edge2 = new Edge(task.point2, task.point2);
			List<Edge> list2 = new List<Edge>();
			list2.Add(edge2);
			task.ways2.Enqueue(list2);
			task.checked1 = new HashSet<string>();
			task.checked2 = new HashSet<string>();
			task.checked1.Add(task.point1);
			task.checked2.Add(task.point2);
			algo.setProblem(task);
		}
		public void setGraph(string path)
		{
			graph = new Graph();
			graph.ReadEdges(path);
		}
		public void Run()
		{
			task = algo.Run(graph);
			if (task.result != " ")
			{
				List<Edge> way = task.getWays();

				string s = "";
				for (int i = 1; i < way.Count-1; i++)
				{
					s = s + way[i].lA.ToString() + "-" + way[i].lB.ToString() + ";";
					//isdone = true;
				}
				Console.WriteLine (s);


			}
		}
		*/
		public void drawWay(List<Edge> way)
		{
			for (int i = 0; i < way.Count; i++)
			{

				Console.WriteLine(way.ElementAt(i).lA);
				Console.WriteLine(way.ElementAt(i).lB);



			}


		}
		static public void graphRedraw(Problem p)
		{




		}
		//
		// This method is invoked when the application is about to move from active to inactive state.
		//
		// OpenGL applications should use this method to pause.
		//
		public override void OnResignActivation (UIApplication application)
		{
		}

		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}

		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}

		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}
	}
}

