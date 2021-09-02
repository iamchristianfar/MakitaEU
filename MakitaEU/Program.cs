using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace MakitaEU
{
    class Program : Block
    {
        static void Main(string[] args)
        {
            var list = Initialize();

            // check case 1 - All convenience buildsings are on the same block

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Gym==true && list[i].Supermarket==true && list[i].School==true)
                {
                    Console.WriteLine("The optimal block is " + (i+1));
                    return;
                }
            }

            // check case 2 - Two convenience buildings are on the same block

            var optimalBlockIndex = -1;
            var minDistance = int.MaxValue;

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Gym == true && list[i].Supermarket == true)
                {
                    (int schoolIndex, int distance) = GetSchoolDistance(list, i);

                    if (schoolIndex !=-1 && distance < minDistance)
                    {
                        optimalBlockIndex = i;
                        minDistance = distance;
                    }
                }

                if (list[i].Gym == true && list[i].School == true)
                {
                    (int supermarketIndex, int distance) = GetSupermarketDistance(list, i);

                    if (supermarketIndex != -1 && distance < minDistance)
                    {
                        optimalBlockIndex = i;
                        minDistance = distance;
                    }
                }

                if (list[i].Supermarket == true && list[i].School == true)
                {
                    (int gymIndex, int distance) = GetGymDistance(list, i);

                    if (gymIndex != -1 && distance < minDistance)
                    {
                        optimalBlockIndex = i;
                        minDistance = distance;
                    }

                }
            }

            if (optimalBlockIndex !=-1)
            {
                Console.WriteLine("The optimal block is: " + (optimalBlockIndex + 1));
            }


            // check case 3 - All convenience buildings are on different blocks 

            optimalBlockIndex = -1;
            minDistance = int.MaxValue;

            for (int i = 0; i < list.Count-2; i++)
            {
                for (int j = i+1; j < list.Count-1; j++)
                {
                    for (int k = j+1; k < list.Count; k++)
                    {
                        var hasSchool = list[i].School || list[j].School || list[k].School;
                        var hasGym = list[i].Gym|| list[j].Gym || list[k].Gym;
                        var hasSupermarket = list[i].Supermarket || list[j].Supermarket || list[k].Supermarket;

                        if (hasSchool && hasGym && hasSupermarket && (k-i <= minDistance))
                        {
                            optimalBlockIndex = j;
                            minDistance = k - j;
                        }
                    }
                }
            }


            if (optimalBlockIndex != -1)
            {
               Console.WriteLine("The optimal block is: " + (optimalBlockIndex + 1));
            }


        }

        private static (int,int) GetSchoolDistance(List<Block> list, int i)
        {
            var schoolIndex = -1;
            var distance = int.MaxValue;

            for (int j = i; j > 0; j--)
            {
                if (list[j].School==true)
                {
                    schoolIndex = j;
                    distance = i - j;
                    break;
                }
            }

            for (int j = i; j < list.Count; j++)
            {
                if (list[j].School == true && j-i < distance)
                {
                    schoolIndex = j;
                    distance = i - j;
                    break;
                }
            }

            return (schoolIndex, distance);
        }

        private static (int, int) GetSupermarketDistance(List<Block> list, int i)
        {
            var supermarketIndex = -1;
            var distance = int.MaxValue;

            for (int j = i; j > 0; j--)
            {
                if (list[j].Supermarket == true)
                {
                    supermarketIndex = j;
                    distance = i - j;
                    break;
                }
            }

            for (int j = i; j < list.Count; j++)
            {
                if (list[j].Supermarket == true && j - i < distance)
                {
                    supermarketIndex = j;
                    distance = i - j;
                    break;
                }
            }

            return (supermarketIndex, distance);
        } 
        
        private static (int, int) GetGymDistance(List<Block> list, int i)
        {
            var gymIndex = -1;
            var distance = int.MaxValue;

            for (int j = i; j > 0; j--)
            {
                if (list[j].Gym == true)
                {
                    gymIndex = j;
                    distance = i - j;
                    break;
                }
            }

            for (int j = i; j < list.Count; j++)
            {
                if (list[j].Gym == true && j - i < distance)
                {
                    gymIndex = j;
                    distance = i - j;
                    break;
                }
            }

            return (gymIndex, distance);
        }

        private static List<Block>  Initialize()
        {
            List<Block> blocks = new List<Block>();
           
            Block block1 = new Block();
            block1.Supermarket = false;
            block1.School = false;
            block1.Gym = true;

            Block block2 = new Block();
            block2.Supermarket = false;
            block2.School = false;
            block2.Gym = true;

            Block block3 = new Block();
            block3.Supermarket = true;
            block3.School = false;
            block3.Gym = true;

            Block block4 = new Block();
            block4.Supermarket = false;
            block4.School = true;
            block4.Gym = false;

            blocks.Add(block1);
            blocks.Add(block2);
            blocks.Add(block3);
            blocks.Add(block4);
          
            return blocks;
        }
    }

	
}
