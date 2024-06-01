using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static int[,] track;
    static int trackHeight;
    static int numRunners;
    static int obstacleDelay = 500; // Задержка в миллисекундах при встрече с препятствием

    static void Main(string[] args)
    {
        trackHeight = 10; // Высота трассы
        numRunners = 5; // Количество бегунов

        // Генерация случайной трассы
        GenerateTrack(trackHeight, numRunners);

        // Запуск потоков-бегунов
        List<Task> runners = new List<Task>();
        for (int i = 0; i < numRunners; i++)
        {
            int runnerId = i + 1;
            runners.Add(Task.Run(() => RunTrack(runnerId)));
        }

        // Ожидание завершения всех бегунов
        Task.WaitAll(runners.ToArray());

        Console.WriteLine("Все бегуны достигли финиша.");
    }

    static void GenerateTrack(int height, int width)
    {
        Random rand = new Random();
        track = new int[height, width];

        // Заполнение трассы препятствиями случайным образом
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                track[i, j] = rand.Next(0, 2); // 0 - нет препятствия, 1 - препятствие
            }
        }

        // Отображение трассы
        Console.WriteLine("Сгенерированная трасса:\n1 2 3 4 5 ---- бегуны \n- СТАРТ -");
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Console.Write(track[i, j] + " ");
            }
            Console.WriteLine();
        }
    }

    static void RunTrack(int runnerId)
    {
        for (int i = 0; i < trackHeight; i++)
        {
            if (track[i, runnerId - 1] == 1)
            {
                // Задержка при встрече с препятствием
                Console.WriteLine($"Бегун {runnerId} встретил препятствие на этапе {i + 1}.");
                Thread.Sleep(obstacleDelay);
            }
        }

        Console.WriteLine($"Бегун {runnerId} достиг финиша!");
    }
}