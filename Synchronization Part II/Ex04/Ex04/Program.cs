using System.Threading;

namespace OS_Sync_01
{
	class Program
	{
		private static string x = "";
		private static int exitflag = 0;
		private static int updateFlag = 0;
		private static object _lock = new object();

		static void ThReadX()
		{
				while (exitflag == 0)
				{
					lock(_lock){
						if (updateFlag == 1) { 
							Console.WriteLine("X = {0}", x);
							updateFlag = 0;
						}
					}
				}
				Console.WriteLine("Thread 1 exit");
		}
		static void ThWriteX()
		{
			string xx;
				while (exitflag == 0)
				{
					lock (_lock) { 
						Console.Write("Input: ");
						xx = Console.ReadLine();
						if (xx == "exit") {
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
			Thread A = new Thread(ThReadX);
			Thread B = new Thread(ThWriteX);

			A.Start();
			B.Start();
		}
	}
}
