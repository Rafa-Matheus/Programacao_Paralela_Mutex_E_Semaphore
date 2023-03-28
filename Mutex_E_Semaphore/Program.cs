using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mutex_E_Semaphore
{
    class Program
    {
        static Mutex mutexTask;
        static Semaphore semaphoreTask;

        /*
        4 Tasks (t1, t2, t3 e t4) estão associadas ao método abaixo (PrintMessageUsigMutex).
        Para garantir que apenas uma Task por vez o execute, utilizamos um objeto
        do tipo Mutex, conforme exemplificado: 
        */
        static void PrintMessageUsingMutex(int taskId)
        {
            mutexTask.WaitOne(); // Apenas uma Task por vez terá domínio sobre o Mutex
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine($"Id da Task: {taskId} | Nº do Contador: {i}");
                Thread.Sleep(100);
            }
            mutexTask.ReleaseMutex(); // Destrava o Mutex
        }

        /*
        No exemplo abaixo é feito o controle de acesso através do Semaphore.
        Com ele é possível determinar o número de Tasks que irão acessar
        simultaneamente o recurso/método que irá rodar em paralelo.
 
        Neste programa, uma nova instância de Semaphore foi criada com um
        contador inicial de 2 e um contador máximo de 2. Isso significa que
        até duas Threads, e não mais do que isso, podem acessar o recurso
        compartilhado simultaneamente, mesmo que haja mais Threads esperando
        por acesso.

        4 Tasks (t5, t6, t7 e t8) estão associadas ao método abaixo (PrintMessageUsignSemaphore)
        */
        static void PrintMessageUsingSemaphore(int taskId)
        {
            semaphoreTask.WaitOne(); // Será acessado por mais de uma Task por vez
            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine($"Id da Task: {taskId} | Nº do Contador: {i}");
                Thread.Sleep(500);
            }
            semaphoreTask.Release(); // Destrava o Semaphore
        }


        static void Main(string[] args)
        {
            
            mutexTask = new Mutex();

            Task t1 = Task.Run(() => PrintMessageUsingMutex(1));
            Task t2 = Task.Run(() => PrintMessageUsingMutex(2));
            Task t3 = Task.Run(() => PrintMessageUsingMutex(3));
            Task t4 = Task.Run(() => PrintMessageUsingMutex(4));

            Console.ReadKey();
            Console.WriteLine();

            /*
            No exemplo abaixo, uma nova instância de Semaphore é criada com um
            contador inicial de 2 e um contador máximo de 2. Isso significa que
            até duas Threads, e não mais do que isso, podem acessar o recurso
            compartilhado simultaneamente, mesmo que haja mais Threads esperando
            por acesso.
            */
            semaphoreTask = new Semaphore(2, 2); // iniCont = 2 e maxCount = 2

            Task t5 = Task.Run(() => PrintMessageUsingSemaphore(5));
            Task t6 = Task.Run(() => PrintMessageUsingSemaphore(6));
            Task t7 = Task.Run(() => PrintMessageUsingSemaphore(7));
            Task t8 = Task.Run(() => PrintMessageUsingSemaphore(8));

            Console.ReadKey();
        }
    }
}
