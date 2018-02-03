using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace ProgramowanieWspolbiezneProjekt_KuskaGawlik21
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var threads = new MyThreads();
            threads.StartThreads();
        }
    }

    public class MyThreads
    {
        private static int _arraySize = 10;
        private static int[] _smallerstValues = new int[3];
        private static object _locker = new object();
        private static Random _random = new Random();

        private static int[] SaveSmalletsValue(int? value, int? threadNumber)
        {
            lock(_locker)
            {
                if (value.HasValue && threadNumber.HasValue)
                {
                    _smallerstValues[threadNumber.Value] = value.Value;
                }
                return _smallerstValues;
            }
        }

        public Thread F = new Thread(() =>
        {
            const int _threadNumber = 0;
            int[] array = new int[_arraySize];
            int[] commonArray = new int[3];
            //var random = new Random();

            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = _random.Next(0, 10);
            }
              
            array = array.OrderByDescending(x => x).ToArray();
            bool isFirstTime = true;

            for (var i = _arraySize - 1; i > 0; i-- )
            {
                commonArray = SaveSmalletsValue(null, null);

                if (isFirstTime || commonArray[1] > array[i] || commonArray[2] > array[i])
                {
                    isFirstTime = false;
                    commonArray = SaveSmalletsValue(array[i], _threadNumber);
                }

                if (commonArray.Distinct().Count() == 1)
                {
                    Console.WriteLine("The smallest value of each thread is:" + commonArray.First());
                    break;
                }

            }
        });


        public Thread G = new Thread(() =>
        {
            const int _threadNumber = 1;
            int[] array = new int[_arraySize];
            int[] commonArray = new int[3];
            //var random = new Random();
            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = _random.Next(0, 10);
            }

            array = array.OrderByDescending(x => x).ToArray();
            bool isFirstTime = true;

            for (var i = _arraySize - 1; i > 0; i--)
            {
                commonArray = SaveSmalletsValue(null, null);

                if (isFirstTime || commonArray[0] > array[i] || commonArray[2] > array[i])
                {
                    isFirstTime = false;
                    commonArray = SaveSmalletsValue(array[i], _threadNumber);
                }

                if (commonArray.Distinct().Count() == 1)
                {
                    break;
                }

            }
        });


        public Thread H = new Thread(() =>
        {
            const int _threadNumber = 2;
            int[] array = new int[_arraySize];
            int[] commonArray = new int[3];
            //var random = new Random();
            for (int i = 0; i < _arraySize; i++)
            {
                array[i] = _random.Next(0, 10);
            }

            array = array.OrderByDescending(x => x).ToArray();
            bool isFirstTime = true;

            for (var i = _arraySize - 1; i > 0; i--)
            {
                commonArray = SaveSmalletsValue(null, null);

                if (isFirstTime || commonArray[0] > array[i] || commonArray[1] > array[i])
                {
                    isFirstTime = false;
                    commonArray = SaveSmalletsValue(array[i], _threadNumber);
                }

                if (commonArray.Distinct().Count() == 1)
                {
                    break;
                }

            }
        });


        public void StartThreads()
        {
            F.Start();
            G.Start();
            H.Start();
        }
    }
}
