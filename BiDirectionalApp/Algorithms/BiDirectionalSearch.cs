
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace BiDirectionalApp
{
	public class Problem
	{

		//The starting point in the graph
		public string start;
		//The endding point in the graph
		public string end;
		//The list that contains all the edge going forward direction
		public Queue<List<Edge>> forwards;
		//The list that contains all the edge going backwards direction
		public Queue<List<Edge>> backwards;
		//Contains the cxwwwheck edge going forwards
		public HashSet<string> checkedforwards;
		//Contains the check edge going backwards
		public HashSet<string> checkedbackwards;
		//Holds the string with the results
		public string result = "  ";

		//Checks if the problem is solved
		public bool isSolved()
		{
			//Checks if their is a intersection for the two arrays
			if (forwardarray().Intersect(backwardarray()).Count() != 0)
			{
				//Create the result and stores it
				result = forwardarray().Intersect(backwardarray()).Last();
			}
			//returns the boolean of the if statement from above
			return forwardarray().Intersect(backwardarray()).Count() != 0;
		}

		//Gets all the edge in the graph
		public List<Edge> getWays()
		{
			//Creates a new instance of edge list
			List<Edge> daways = new List<Edge>();
			//For loop to go though the forwards array
			for (int j = 0; j < forwards.Count; j++)
			{
				//Checks if last element equal to result string in forwards array
				if (forwards.ElementAt(j).Last().lB == result)
				{
					//Sets the array to elements
					daways=(forwards.ElementAt(j));
				}
			}
			//For loop to go though the backwards array
			for (int j = 0; j < backwards.Count; j++)
			{
				//Checks if last element equal to result string in backwards array
				if (backwards.ElementAt(j).Last().lB == result)
				{
					//For loop to go though the elements in the backwards array
					for (int i = backwards.ElementAt(j).Count-1; i >= 0; i--)
					{
						//Add the current element to the local instance array of edge objects
						daways.Add(backwards.ElementAt(j)[i]);
					}
					//Break the loop
					break;
				}
			}
			//Returns the array list
			return daways;
		}

		//Return the forward of element in the gragh
		public Queue<string> forwardarray()
		{
			Queue<string> fa = new Queue<string>();
			fa.Enqueue(start);
			for (int i = 0; i < forwards.Count; i++)
			{
				fa.Enqueue(forwards.ElementAt(i).Last().lB);
			}
			return fa;
		}

		//Return the backwards of element in the gragh
		public Queue<string> backwardarray()
		{
			Queue<string> ba = new Queue<string>();
			ba.Enqueue(end);
			for (int i = 0; i < backwards.Count; i++)
			{
				ba.Enqueue(backwards.ElementAt(i).Last().lB);
			}
			return ba;
		}
	}

	//The graph class
	public class Graph
	{
		//List of all the edges in the gragh
		List<Edge> edges;

		//Functions that finds all the neighbors in the gragh
		public List<string> Neighbors(string a)
		{
			//Creates a new insance of the array
			List<string> rez = new List<string>();
			//foreach loop for a edge in the array of edges
			foreach (Edge edge in edges)
			{
				//Checks if the end of the other edge are equal to the other edges
				if (edge.lA == a || edge.lB == a)
				{
					//If some it adds it to the array to return
					rez.Add(edge.getOther(a));
				}
			}
			//Returns the array of findNeighbors
			return rez;
		}

		//Basic constructor of the graph class
		public Graph()
		{
			//Sets list array to a new instance of edges
			edges = new List<Edge>();
		}

		//Reads in a file name and parse the data for the graph
		public void ReadEdges(string file)
		{
			var documents =
				Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments);

			var filename = Path.Combine (documents, "test.txt");
			System.IO.StreamReader myFile = new System.IO.StreamReader(filename);
			while (!myFile.EndOfStream)
			{
				string edge = myFile.ReadLine();
				Edge e = new Edge(edge.Split(' ')[0], edge.Split(' ')[1], double.Parse(edge.Split(' ')[2]));
				edges.Add(e);
			}
			myFile.Close();
		}

		//Creates of a edge all ready exist in the edge array to same space and time
		public double edgeExists(string a, string b)
		{
			//Sets the rez double to -1
			double existarray = -1;
			//Creates a temp edge to compare with in the for loop
			Edge current = new Edge(a, b, 0);
			//For loop that going though all the edges
			for (int i = 0; i < edges.Count; i++)
			{
				//Checks if the two edge are the same
				if (edges[i].Equals(current))
				{
					//Sets the weight to return if it exist
					existarray = edges[i].weight;
				}
			}
			//Returns the value
			return existarray;
		}
	}
}
public class Edge
{
	//The name of point A
	public string lA;
	//The name of point B
	public string lB;
	//The line weight on the gragh
	public double weight;

	//Configures the Edge node
	public Edge(string l1, string l2, double w)
	{
		//Sets the value in the new instance
		lA = l1;
		lB = l2;
		weight = w;
	}
	public Edge(string l1, string l2)
	{
		lA = l1;
		lB = l2;
		weight = 0;
	}
	public string getOther(string a)
	{
		if (a != lA)
			return lA;
		else
			return lB;
	}
	//Checks if the edge against the other edge 
	public bool isEqual(Edge other)
	{
		//If statment to check if the two different edge are the same and to not add to the array
		if ((lA == other.lA && lB == other.lB) || (lA == other.lB && lB == other.lA))
			//Returns that it exists
			return true;
		//Returns that it does not exists
		else return false;
	}
}