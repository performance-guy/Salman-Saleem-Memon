using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1_task_2
{
    class Array
    {
        int[] a = new int[10];
        int[] schk = new int[10];
        public void Ask()
        {

            Console.WriteLine("Fill number data!");
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = Convert.ToInt32(Console.ReadLine());
            }


        }
        public void sumchk()
        {
            for (int i = 0; i < a.Length; i++)
            {
                if
                    (a[i] <= 25)
                {
                    schk[i] = a[i];
                }
            }
            //for (int i = 0; i < schk.Length; i++)
            //{
            //    Console.WriteLine(schk[i]);

            //}
            //Console.ReadLine();
            for (int i = 0; i < schk.Length; i++)
            {
                for (int j = i + 1; j < schk.Length; j++)
                {
                    if
                        (schk[i] + schk[j] == 25)
                    {
                        Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3}", i, schk[i], j, schk[j]);
                    }
                    else
                    {
                        for (int k = i + 2; k < schk.Length; k++)
                        {
                            if
                                (schk[i] + schk[j] + schk[k] == 25)
                            {
                                Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3},\n3:{4} , {5}", i, schk[i], j, schk[j], k, schk[k]);
                            }
                            else
                            {
                                for (int l = i + 3; l < schk.Length; l++)
                                {
                                    if
                                (schk[i] + schk[j] + schk[k] + schk[l] == 25)
                                    {
                                        Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3},\n3:{4} , {5}", i, schk[i], j, schk[j], k, schk[k]);
                                        Console.WriteLine("4:{0} , {1}", l, schk[l]);
                                    }
                                    else
                                    {
                                        for (int z = i + 4; z < schk.Length; z++)
                                        {
                                            if
                                        (schk[i] + schk[j] + schk[k] + schk[l] + schk[z] == 25)
                                            {
                                                Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3},\n3:{4} , {5}", i, schk[i], j, schk[j], k, schk[k]);
                                                Console.WriteLine("4:{0} , {1}", l, schk[l]);
                                                Console.WriteLine("5:{0} , {1}", z, schk[z]);
                                            }
                                            else
                                            {
                                                for (int x = i + 5; x < schk.Length; x++)
                                                {
                                                    if
                                                (schk[i] + schk[j] + schk[k] + schk[l] + schk[z] + schk[x] == 25)
                                                    {
                                                        Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3},\n3:{4} , {5}", i, schk[i], j, schk[j], k, schk[k]);
                                                        Console.WriteLine("4:{0} , {1}", l, schk[l]);
                                                        Console.WriteLine("5:{0} , {1}", z, schk[z]);
                                                        Console.WriteLine("6:{0} , {1}", x, schk[x]);
                                                    }
                                                    else
                                                    {
                                                        for (int c = i + 6; c < schk.Length; c++)
                                                        {
                                                            if
                                                        (schk[i] + schk[j] + schk[k] + schk[l] + schk[z] + schk[x] + schk[c] == 25)
                                                            {
                                                                Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3},\n3:{4} , {5}", i, schk[i], j, schk[j], k, schk[k]);
                                                                Console.WriteLine("4:{0} , {1}", l, schk[l]);
                                                                Console.WriteLine("5:{0} , {1}", z, schk[z]);
                                                                Console.WriteLine("6:{0} , {1}", x, schk[x]);
                                                                Console.WriteLine("7:{0} , {1}", c, schk[c]);

                                                            }
                                                            else
                                                            {
                                                                for (int v = i + 7; v < schk.Length; v++)
                                                                {
                                                                    if
                                                                (schk[i] + schk[j] + schk[k] + schk[l] + schk[z] + schk[x] + schk[c] + schk[v] == 25)
                                                                    {
                                                                        Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3},\n3:{4} , {5}", i, schk[i], j, schk[j], k, schk[k]);
                                                                        Console.WriteLine("4:{0} , {1}", l, schk[l]);
                                                                        Console.WriteLine("5:{0} , {1}", z, schk[z]);
                                                                        Console.WriteLine("6:{0} , {1}", x, schk[x]);
                                                                        Console.WriteLine("7:{0} , {1}", c, schk[c]);
                                                                        Console.WriteLine("8:{0} , {1}", v, schk[v]);
                                                                    }
                                                                    else
                                                                    {
                                                                        for (int b = i + 8; b < schk.Length; b++)
                                                                        {
                                                                            if
                                                                        (schk[i] + schk[j] + schk[k] + schk[l] + schk[z] + schk[x] + schk[c] + schk[v] + schk[b] == 25)
                                                                            {
                                                                                Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3},\n3:{4} , {5}", i, schk[i], j, schk[j], k, schk[k]);
                                                                                Console.WriteLine("4:{0} , {1}", l, schk[l]);
                                                                                Console.WriteLine("5:{0} , {1}", z, schk[z]);
                                                                                Console.WriteLine("6:{0} , {1}", x, schk[x]);
                                                                                Console.WriteLine("7:{0} , {1}", c, schk[c]);
                                                                                Console.WriteLine("8:{0} , {1}", v, schk[v]);
                                                                                Console.WriteLine("9:{0} , {1}", b, schk[b]);
                                                                            }
                                                                            else
                                                                            {
                                                                                for (int n = i + 9; n < schk.Length; n++)
                                                                                {
                                                                                    if
                                                                                (schk[i] + schk[j] + schk[k] + schk[l] + schk[z] + schk[x] + schk[c] + schk[v] + schk[b] + schk[n] == 25)
                                                                                    {
                                                                                        Console.WriteLine("Index number & Element\n1:{0} , {1}\n2:{2} , {3},\n3:{4} , {5}", i, schk[i], j, schk[j], k, schk[k]);
                                                                                        Console.WriteLine("4:{0} , {1}", l, schk[l]);
                                                                                        Console.WriteLine("5:{0} , {1}", z, schk[z]);
                                                                                        Console.WriteLine("6:{0} , {1}", x, schk[x]);
                                                                                        Console.WriteLine("7:{0} , {1}", c, schk[c]);
                                                                                        Console.WriteLine("8:{0} , {1}", v, schk[v]);
                                                                                        Console.WriteLine("9:{0} , {1}", b, schk[b]);
                                                                                        Console.WriteLine("10:{0} , {1}", n, schk[n]);
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public void ShowElements()
        {

            Console.WriteLine("List of filled data!");
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
        }
        public void showEven()
        {
            for (int i = 0; i < a.Length; i++)
            {
                if
               (a[i] % 2 == 0)
                {
                    Console.WriteLine(a[i]);
                }
                
            }
        }
        public void ShowODD()
        {
            for (int i = 0; i < a.Length; i++)
            {
                 if
               (a[i] % 2 != 0)
                {
                    Console.WriteLine(a[i]);
                }
            }
        }
        public void Average()
        {
            double q = 0;
            for (int i = 0; i < a.Length; i++)
            {
                q = q + a[i];
            }
            Console.WriteLine("Average:");
            Console.WriteLine(q / a.Length);
        }
        public void Insert(int index, int value)
        {
            
            if
                (index>a.Length)
            {
                Console.WriteLine("Index out of bound");

            }
            else
            {
                for (int i = a.Length - 1; i > index; i--)
                {
                    if
                        (i - 1 >= index)
                    {
                        a[i] = a[i-1];
                    }


                }
                a[index] = value;
            }
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
        }
        public void SearchDelete(int n1, int n2)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if
                    (a[i]==n1)
                {
                    for (int j = i; j < a.Length; j++)
                    {
                        if (j + 1 < a.Length)
                        {
                            a[j] = a[j + 1];
                        }
                        else
                            a[j] = 0;
                    }
                }
            }
            for (int i = 0; i < a.Length; i++)
            {
                if
                    (a[i] == n2)
                {
                    for (int j = i; j < a.Length; j++)
                    {
                        if (j + 1 < a.Length)
                        {
                            a[j] = a[j + 1];
                        }
                        else
                            a[j] = 0;
                    }
                }
            }
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine(a[i]);
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Array A = new Array();
            A.Ask();
            Console.WriteLine("Select Correct number according to correct choice");
            Console.WriteLine("1:Sum Check to 25?");
            Console.WriteLine("2:Even & Odd Check?");
            Console.WriteLine("3:Average Check?");
            Console.WriteLine("4:value Insert?");
            Console.WriteLine("5:Value Delete?");

            char choice = Convert.ToChar(Console.ReadLine());
            switch (choice)
            {
                case'1':
                    A.sumchk();
                    break;
                case'2':
                    Console.WriteLine("Even numbers are:");
                    A.showEven();
                    Console.WriteLine("Odd Numbers are:");
                    A.ShowODD();
                    break;
                case'3':
                    A.Average();
                    break;
                case'4':
                    Console.Clear();
                    Console.WriteLine("index value ?");
                    int i = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Value ?");
                    int v = Convert.ToInt32(Console.ReadLine());
                    A.Insert(i, v);
                    break;
                case'5':
                    Console.WriteLine("give 2 value?");
                    int n1 = Convert.ToInt32(Console.ReadLine());
                    int n2 = Convert.ToInt32(Console.ReadLine());
                    A.SearchDelete(n1, n2);
                    break;
                default:
                    Console.WriteLine("Invalid Selection!");
                    break;
            }
            //A.SearchDelete(14, 15);
            //A.Insert(3, 20);
            //A.Average();
            //A.showEven();
            //Console.ReadLine();
            //A.ShowElements();
            //A.sumchk();
            
        }
    }
}
