﻿using System;
using System.Threading;

namespace cv_lab
{
	class Program
	{
		private static string x = "";
		private static int exitflag = 0;
		private static int updateFlag = 0;
		private static object _lock = new object();

		static void ThReadX(object i)
		{
			while (exitflag == 0)
			{
				lock (_lock)
				{
					if (x != "exit" && updateFlag == 1)
					{
						Console.WriteLine("***Thread {0} : x = {1}***", i, x);
						updateFlag = 0;
					}
				}
			}
			Console.WriteLine("---Thread {0} exit---", i);

		}
		static void ThWriteX()
		{
			string xx;
			while (exitflag == 0)
			{
				lock (_lock)
				{
					Console.Write("Input: ");
					xx = Console.ReadLine();
					if (xx == "exit")
					{
						exitflag = 1;
					}
					else
					{
						x = xx;
						updateFlag = 1;
					}
				}
			}
		}
		static void Main(string[] args)
		{
			Thread A = new Thread(ThWriteX);
			Thread B = new Thread(ThReadX);
			Thread C = new Thread(ThReadX);
			Thread D = new Thread(ThReadX);

			A.Start();
			B.Start(1);
			C.Start(2);
			D.Start(3);
		}
	}
}
