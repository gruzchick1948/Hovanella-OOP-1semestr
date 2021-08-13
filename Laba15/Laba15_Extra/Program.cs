using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Laba14_Extra
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //На складе имеются товары (файл с записями). Создайте три потока - машины,
            // каждая машина имеет свою скорость загрузки/разгрузки. Разгрузите склад.Обеспечьте последовательный доступ складу //
            // (только одна машина может загружаться единовременно)
            UnloadTheCargo();
        }

        private static void UnloadTheCargo()
        {
            var storage = File.ReadAllLines(@"../../../Storage.txt").ToList();
            int firstMachineSpeed = 1, secondMachineSpeed = 2, thirdMachineSpeed = 3;

            var first = new Thread(FirstMachine);
            var second = new Thread(SecondMachine);
            var third = new Thread(ThirdMachine);
            first.Start();
            first.Join();
            second.Start();
            second.Join();
            third.Start();
            third.Join();


            void FirstMachine()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                for (var i = 0; i < firstMachineSpeed; i++)
                    Console.WriteLine($"First Machine has unloaded {storage[i]}");
                int NumberofUnloadedCargo = firstMachineSpeed <= storage.Count ? firstMachineSpeed : storage.Count;
                storage.RemoveRange(0, firstMachineSpeed);
            }

            void SecondMachine()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                for (var i = 0; i < secondMachineSpeed; i++)
                    Console.WriteLine($"Second Machine has unloaded {storage[i]}");
                int NumberofUnloadedCargo = secondMachineSpeed <= storage.Count ? secondMachineSpeed : storage.Count;
                storage.RemoveRange(0, secondMachineSpeed);
            }

            void ThirdMachine()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                for (var i = 0; i < thirdMachineSpeed; i++)
                    Console.WriteLine($"Third Machine has unloaded {storage[i]}");
                int NumberofUnloadedCargo = thirdMachineSpeed <= storage.Count ? thirdMachineSpeed : storage.Count;
                storage.RemoveRange(0, thirdMachineSpeed);
            }
        }

        private static bool IsStorageEmpty(List<string> storage)
        {
            return storage.Count == 0;
        }
    }
}