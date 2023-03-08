using System;
using System.Runtime.ExceptionServices;

namespace Linked_List
{
    public class Program
    {


        static void Main(string[] args)
        {
            #region Menu
            int choice;
            while (true)
            {
                Console.WriteLine($@"WELCOME:
1) Manual.
2) automatic
3) Close");
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException) { Console.WriteLine("The input you entered wasnt valid [format], program closed."); return; }
                catch (OverflowException) { Console.WriteLine("The input you entered wasnt valid [out of int range], program closed."); return; }
                switch (choice)
                {
                    case 1:
                        StartManual();
                        break;
                    case 2:
                        StartAutomatic();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Choice out of range.");
                        return;

                }
                EnterAndClear();
            }  
            #endregion
        }


        #region  Menu Functions
        static void ShowFunctionsMenu<T>(Node<T> firstList, Node<T> secondList) where T : IComparable<T>
        {
            int choice;
            while (true)
            {

                Console.WriteLine($@"               Select a function and see what happens: 
             -------------------------------------------

*********************************************************************
*                                                                   *
* 0. To Exit.                                                       *
* 1. Returning the length of the list.                              *
* 2. Printing the list.                                             *
*********************************************************************
* 3. Adding a value at the beginning of the list.                   *
* 4. Adding a value at the end of the list.                         *
* 5. Adding a value in the middle of the list.                      *
*********************************************************************
* 6. Deleting an entry at the beginning of the list.                *
* 7. Deleting a value at the end of the list.                       *
* 8. Deleting a value in the middle of the list.                    *
*********************************************************************
* 9. Returning the value from the start of the list [first value].  *
* 10. Returning the value at the end of the list [last value].      *
* 11. Returning the value according to the receiving index.         *
*********************************************************************
* 12. Checking whether the sent value exists in the list or not.    *
* 13. Checking whether the list sent is a circular list.            *
*********************************************************************
* 14. Filtering the duplicate values in the list.                   *
* 15. Get a new list that is a duplicate of the list you sent.      *
* 16. Reverse original list.                                        *
* 17. Sorting the list from smallest to largest.                    *
*********************************************************************
* 18. Are Two Lists Are The Same In Terms Of Content.               *
* 19. Merge Two Lists                                               *
* 20. Merge Two Lists without Duplicates                            *
* 21. Merge Lists Equal Values No rep                               *
*********************************************************************");
                choice = InputChoice("Enter your Choice: ");
                Console.Clear(); if (choice == 0) break;
                bool isWorker = typeof(T) == typeof(Worker);
                switch (choice)
                {
                    case 1:
                        PrintList(firstList);
                        Console.WriteLine("The length of the Firstlist =" + GetListLength(firstList) + "\n");
                        PrintList(secondList);
                        Console.WriteLine("The length of the Secondlist =" + GetListLength(secondList));
                        break;
                    case 2:
                        Console.WriteLine("Print Firstlist");
                        PrintList(firstList);
                        Console.WriteLine("\nPrint Secondlist");
                        PrintList(secondList);
                        break;
                    case 3:
                        Console.WriteLine("Add Node to the start of the list\n");
                        PrintList(firstList);
                        firstList = isWorker ? AddFirst<T>(firstList, GetNewWorkerFromUser<T>()) :
                                               AddFirst<T>(firstList, GetIntFromUser<T>());
                        PrintList(firstList);
                        break;
                    case 4:
                        Console.WriteLine("Add Node to the end of the list\n");
                        PrintList(firstList);
                        firstList = isWorker ? AddLast<T>(firstList, GetNewWorkerFromUser<T>()) :
                                               AddLast<T>(firstList, GetIntFromUser<T>());
                        PrintList(firstList);
                        break;
                    case 5:
                        PrintList(firstList);
                        Console.Write("Enter a index to place the new value: ");
                        int index = int.Parse(Console.ReadLine());
                        if (isWorker)
                        {
                            AddAfter<T>(ReturnNodeByIndex(firstList, index - 1), GetNewWorkerFromUser<T>());
                        }
                        else
                        {
                            AddAfter<T>(ReturnNodeByIndex(firstList, index - 1), GetIntFromUser<T>());
                        }
                        PrintList(firstList);
                        break;
                    case 6:
                        Console.WriteLine("Delete first value and print the change list.");
                        PrintList(firstList);
                        firstList = DeleteFirst(firstList);
                        PrintList(firstList);
                        break;
                    case 7:
                        Console.WriteLine("Delete last value and print the change list.");
                        PrintList(firstList);
                        firstList = DeleteLast(firstList);
                        PrintList(firstList);
                        break;
                    case 8:
                        Console.WriteLine("Delete value by index and print the change list.");
                        PrintList(firstList);
                        index = InputChoice("Enter a index to place the new value: ");
                        DeleteAfter(ReturnNodeByIndex(firstList, index - 1));
                        PrintList(firstList);
                        break;
                    case 9:
                        PrintList(firstList);
                        Console.WriteLine("List first value = " + GetFirstValue(firstList));
                        break;
                    case 10:
                        PrintList(firstList);
                        Console.WriteLine("List last value = " + GetLastValue(firstList));
                        break;
                    case 11:
                        PrintList(firstList);
                        index = InputChoice("Enter index and get the value: ");
                        Console.WriteLine("the value in " + index + " index is " + GetValueByIndex(firstList, index));//
                        break;
                    case 12:
                        T value;
                        PrintList(firstList);
                        if (isWorker)
                        {
                            Console.WriteLine("checked if value exists on the list ");
                            value = GetNewWorkerFromUser<T>();
                            Console.WriteLine($"Is value {value} exist = " + IsValueExists(firstList, value));
                            Console.WriteLine("\nadding worker to list");
                        }
                        else
                        {
                            value = GetIntFromUser<T>();
                            Console.WriteLine($"Is value {value} exist = " + IsValueExists(firstList, value));
                        }
                        break;
                    case 13:
                        Console.WriteLine("Turns the list into a circular list");
                        MakeListCircular(firstList);
                        Console.WriteLine("Is list is circular = " + IsListCircular(firstList));
                        Console.WriteLine("\nTurns the list into a not a circular list");
                        MakeListNotCircular(firstList);
                        Console.WriteLine("Is list is circular = " + IsListCircular(firstList));
                        break;
                    case 14:
                        Console.WriteLine("Original list");
                        PrintList(firstList);
                        Node<T> ListNoDup = GetNewListNoDup(firstList);
                        Console.WriteLine("List no Dup");
                        PrintList(ListNoDup);
                        break;
                    case 15:
                        Console.WriteLine("Original list");
                        PrintList(firstList);
                        Node<T> copyList = CopyList(firstList);
                        Console.WriteLine("The list has been copied");
                        Console.WriteLine("Hard Copy list");
                        PrintList(firstList);
                        break;
                    case 16:
                        Console.WriteLine("Original list");
                        PrintList(firstList);
                        Console.WriteLine("Reverse list");
                        firstList = ReverseOriginalList(firstList);
                        PrintList(firstList);
                        break;
                    case 17:
                        Console.WriteLine("Original list");
                        PrintList(firstList);
                        SortList(firstList);
                        Console.WriteLine(" Sorted list");
                        PrintList(firstList);
                        break;
                    case 18:
                        Console.WriteLine("Print Firstlist");
                        PrintList(firstList);
                        Console.WriteLine("\nPrint Secondlist");
                        PrintList(secondList);
                        Console.WriteLine("\nAre the two list the same in terms of content = " + AreTwoListsAreTheSame(firstList, secondList));
                        break;
                    case 19:
                        Console.WriteLine("Print Firstlist");
                        PrintList(firstList);
                        Console.WriteLine("\nPrint Secondlist");
                        PrintList(secondList);
                        firstList = MergeTwoLists<T>(firstList, secondList);
                        Console.WriteLine("\nThe new merge list:");
                        PrintList(firstList);
                        break;
                    case 20:
                        Console.WriteLine("Print Firstlist");
                        PrintList(firstList);
                        Console.WriteLine("\nPrint Secondlist");
                        PrintList(secondList);
                        firstList = MergeTwoListsNoDuplicates<T>(firstList, secondList);
                        Console.WriteLine("\nThe new merge list without duplicates:");
                        PrintList(firstList);
                        break;
                    case 21:
                        Console.Write("The first list:");
                        PrintList<T>(firstList);
                        Console.Write("The second list:");
                        PrintList<T>(secondList);
                        firstList = MergeListsEqualValues<T>(firstList, secondList);
                        Console.WriteLine("\nThe new merge list (merge only the same values):");
                        PrintList(firstList);
                        break;
                    default:
                        break;
                }
                Console.WriteLine("Enter any Key to continue");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void StartAutomatic()//A function that manipulates all the lists automatically and does not require input from the user
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine(@"Select the list you want to run
1.Worker List.
2.Int List.
3.Student List.
4.Classes List.
5 Classes Array.
6.Exit.");

                int listChoice;

                try
                {
                    listChoice = InputChoice("Enter your Choice: ");
                    Console.Clear();
                }
                catch (Exception)
                {
                    Console.WriteLine("Not a valid choice");
                    listChoice = 7;
                }
                if (listChoice == 6) break;
                switch (listChoice)
                {
                    case 1:
                        WorkerListFunctionsCheck();
                        break;
                    case 2:
                        IntListFunctionsCheck();
                        break;
                    case 3:
                        StudensListFunctionsCheck();
                        break;
                    case 4:
                        ClassesListFunctionsCheck();
                        break;
                    case 5:
                        ClassesArrayFunctionsCheck();
                        break;
                }
                EnterAndClear();
            }

        }

        static void StartManual()//A function that requires input of details of the list creation
        {
            int choice;
            Node<Worker> workerList1 = null;
            Node<Worker> workerList2 = null;
            Node<int> intList1 = null;
            Node<int> intList2 = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine($@"WELCOME TO THE MANUAL LAND OF NODES:
1) Create a list(Int or Worker).
2) Run the functions and see what happens - int
3) Run the functions and see what happens - worker
4) Students List
5) Classes List.
6) Classes Array.      
0) Exit.");
                choice = InputChoice("Enter Your Choice: ");
                if (choice == 0) break;
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($@"Creating lists:
1] Create a Worker list.
2] Create a int list.
3] Exit.");
                        choice = InputChoice("Enter Your Choice: ");
                        if (choice == 3) break;
                        Console.Clear();
                        int listNum = InputChoice("Enter 1 to choose list1 or 2 to choose list2: ");
                        switch (choice)
                        {
                            case 1:
                                if (listNum == 1) workerList1 = CreateWorkerList(workerList1);
                                else if (listNum == 2) workerList2 = CreateWorkerList(workerList2);
                                break;
                            case 2:
                                if (listNum == 1) intList1 = CreateIntList(intList1);
                                else if (listNum == 2) intList2 = CreateIntList(intList2);
                                break;
                            case 3:
                                break;
                        }
                        break;
                    case 2:
                        ShowFunctionsMenu(intList1, intList2);
                        break;
                    case 3:
                        ShowFunctionsMenu(workerList1, workerList2);
                        break;
                    case 4:
                        StudensListFunctionsCheck();
                        break;
                    case 5:
                        ClassesListFunctionsCheck();
                        break;
                    case 6:
                        ClassesArrayFunctionsCheck();
                        break;
                }

            }
        }
        #endregion

        #region  Check functions on IntList [1 - 21]
        public static void IntListFunctionsCheck()
        {

            Console.WriteLine("Creating int list ...\n");
            Node<int> intNode1 = new Node<int>(1);
            Node<int> intNode2 = new Node<int>(2);
            Node<int> intNode3 = new Node<int>(3);
            Node<int> intNode4 = new Node<int>(3);

            Node<int> intList = intNode1;

            intNode1.SetNext(intNode2);
            intNode2.SetNext(intNode3);
            intNode3.SetNext(intNode4);

            MessageInColor("\n------------------------------1----------------------------\n", "Magenta");
            Console.WriteLine("The length of list:" + GetListLength(intList));
            Enter();
            MessageInColor("\n------------------------------2----------------------------\n", "Magenta");
            Console.WriteLine("Print int list:");
            PrintList<int>(intList);
            EnterAndClear();
            MessageInColor("\n------------------------------3----------------------------\n", "Magenta");
            MessageInColor("Adding number 15 to the start of the list by reference:\n", "Cyan");
            AddFirstByRef(ref intList, 15);
            Console.WriteLine("Print new int list:");
            PrintList<int>(intList);
            Enter();
            MessageInColor("\nAdding number 17 to the start of the list not by reference:\n", "Green");
            intList = AddFirst(intList, 17);
            Console.WriteLine("Print new int list:");
            PrintList<int>(intList);
            Enter();
            MessageInColor("\n------------------------------4----------------------------\n", "Magenta");
            MessageInColor("\nAdding number 50 to the the end of the list: (regular function)\n", "Cyan");
            intList = AddLast<int>(intList, 50);
            Console.WriteLine("Print new int list:");
            PrintList<int>(intList);
            Enter();
            MessageInColor("\nAdding number 60 to the the end of the list: (recursive function)\n", "Green");
            intList = RecAddLast<int>(intList, 60);
            Console.WriteLine("Print new int list:");
            PrintList<int>(intList);
            EnterAndClear();
            MessageInColor("\n------------------------------5----------------------------\n", "Magenta");
            Console.WriteLine("Adding a number 10 to the the list after the Node we sent: (Node4 sent) \n");
            PrintList<int>(intList);
            AddAfter<int>(intNode2, 10);
            Console.WriteLine("\nPrint new int list:");
            PrintList<int>(intList);
            Enter();
            MessageInColor("\n------------------------------6----------------------------\n", "Magenta");
            PrintList(intList);
            Console.WriteLine("Delete a number from the start of the list\n");
            intList = DeleteFirst<int>(intList);
            Console.WriteLine("Print new int list:");
            PrintList<int>(intList);
            Enter();
            MessageInColor("\n------------------------------7----------------------------\n", "Magenta");
            Console.WriteLine("Delete the next Node from the Node I sent. (Node3 sent)\n");
            DeleteAfter<int>(intNode2);
            Console.WriteLine("Print new int list:");
            PrintList<int>(intList);
            Enter();
            MessageInColor("\n------------------------------8----------------------------\n", "Magenta");
            MessageInColor("\n\"Delete a value from the end of the list\n", "Cyan");
            intList = DeleteLast<int>(intList);
            Console.WriteLine("Print new int list:");
            PrintList<int>(intList);
            Enter();
            MessageInColor("\nDelete a value from the end of the list [using recursive function]\n", "Green");
            intList = RecDeleteLast<int>(intList);
            Console.WriteLine("Print new int list:");
            PrintList<int>(intList);
            EnterAndClear();
            MessageInColor("\n------------------------------9----------------------------\n", "Magenta");
            Console.WriteLine("Current list");
            PrintList<int>(intList);
            Console.WriteLine("The first value of the list: " + GetFirstValue(intList));
            Enter();
            MessageInColor("\n------------------------------10----------------------------\n", "Magenta");
            //Console.WriteLine("The last value of the list: " + GetLastValue<int>(null)); // --> אם נשלח רשימה שהיא ריקה נקבל חריגה בהתאם למה שמוגדר בפונקציה
            MessageInColor("The last value of the list: (regular function) = " + GetLastValue(intList), "Cyan");
            Console.WriteLine();
            Enter();
            MessageInColor("\nThe last value of the list: (recursive function) = " + RecGetLastValue(intList), "Green");
            Enter();
            MessageInColor("\n------------------------------11----------------------------\n", "Magenta");
            Console.WriteLine("Show values by index");
            PrintList<int>(intList);
            Console.WriteLine("The value in index 3: " + RecGetValueByIndex(intList, 3));
            Console.WriteLine("The value in index 0: " + GetValueByIndex(intList, 0));
            Console.WriteLine("The value in last index: " + GetValueByIndex(intList, GetListLength(intList) - 1));
            Enter();
            // exception
            //Console.WriteLine("The value in index -1: " + GetValueByIndex(intList, -1));
            //Console.WriteLine("The value in index 50: " + GetValueByIndex(intList, 50));
            //Console.WriteLine("The value in last index: " + GetValueByIndex<int>(null, GetListLength(intList)));
            //Enter();
            MessageInColor("\n------------------------------12----------------------------\n", "Magenta");
            int numToCheck = 99;
            MessageInColor("\n(regular function)", "Cyan");
            Console.WriteLine("Checking if the value exists in the array");
            PrintList<int>(intList);
            Console.WriteLine($"Is {numToCheck} exist in the list = {IsValueExists(intList, numToCheck)}\n");
            Console.WriteLine("Adding the value to the list and cheking again");
            intList = AddLast(intList, numToCheck);
            PrintList<int>(intList);
            Console.WriteLine($"Is {numToCheck} exist in the list = {IsValueExists(intList, numToCheck)}");
            Enter();
            numToCheck = 150;
            MessageInColor("\n(recursive function)", "Green");
            Console.WriteLine("Checking if the value exists in the array");
            Console.WriteLine($"Is {numToCheck} exist in the list = {RecIsValueExists(intList, numToCheck)}\n");
            Console.WriteLine("Adding the value to the list and cheking again");
            intList = AddLast(intList, numToCheck);
            PrintList<int>(intList);
            Console.WriteLine($"Is {numToCheck} exist in the list = {RecIsValueExists(intList, numToCheck)}");
            EnterAndClear();
            MessageInColor("\n------------------------------13----------------------------\n", "Magenta");
            Console.WriteLine("Checking whether the list sent is a circular list");
            PrintList(intList);
            Console.WriteLine("Is list circular: " + RecIsListCircular(intList) + "\n");
            Console.WriteLine("Make a circular list");
            Node<int> intCircularList = new Node<int>(57);
            intCircularList = AddLast(intCircularList, 69);
            intCircularList = AddLast(intCircularList, 67);
            intCircularList.GetNext().GetNext().SetNext(intCircularList);
            PrintCircularList(intCircularList);
            Console.WriteLine("\nAdd 18 To the last of Circular List ");
            CircularListAddLast(intCircularList, 18);
            PrintCircularList(intCircularList);
            Console.WriteLine("\nAdd 77 to the First of Circular List ");
            intCircularList = CircularAddFirst(intCircularList, 77);
            PrintCircularList(intCircularList);
            Console.WriteLine("\nDelete the First of Circular List ");
            intCircularList = CircularDeleteFirst(intCircularList);
            PrintCircularList(intCircularList);
            Console.WriteLine("\nDelete the last of Circular List ");
            CircularDeleteLast(intCircularList);
            PrintCircularList(intCircularList);
            Console.WriteLine("\nIs list circular: " + RecIsListCircular(intCircularList) + "\n");
            EnterAndClear();
            MessageInColor("\n------------------------------14----------------------------\n", "Magenta");
            intList = AddLast(intList, 12);
            intList = AddLast(intList, 12);
            Console.WriteLine("Current list");
            PrintList(intList);
            Console.WriteLine("Filtering the duplicate values in the list");
            Node<int> noDupIntList = GetNewListNoDup(intList);
            PrintList(noDupIntList);
            Enter();
            MessageInColor("\n------------------------------15----------------------------\n", "Magenta");
            Console.WriteLine("Returning a new list that is a duplicate of the list you sent");
            Console.WriteLine("The original list");
            PrintList(intList);
            Node<int> copyIntList = CopyList(intList);
            Console.WriteLine("The copy list");
            PrintList(copyIntList);
            Enter();
            MessageInColor("\n------------------------------16----------------------------\n", "Magenta");
            MessageInColor("\n(regular function)\n", "Cyan");
            Console.WriteLine("Turning the list from the end to the beginning without creating a new list");
            Console.WriteLine("The original list");
            PrintList(intList);
            Console.WriteLine("The reverse list");
            intList = ReverseOriginalList(intList);
            PrintList(intList);
            Enter();
            MessageInColor("\n(regular function)\n", "Green");
            Console.WriteLine("The original list");
            PrintList(intList);
            Console.WriteLine("The reverse list");
            intList = RecReverseOriginalList(intList);
            PrintList(intList);
            EnterAndClear();
            MessageInColor("\n------------------------------17----------------------------\n", "Magenta");
            Console.WriteLine("Sorting the list from smallest to largest");
            Console.WriteLine("The original list");
            PrintList(intList);
            Console.WriteLine("The Sorted list");
            SortList<int>(intList);
            PrintList<int>(intList);
            EnterAndClear();
            MessageInColor("\n------------------------------18----------------------------\n", "Magenta");
            Console.WriteLine("Are the two lists the same in terms of content (the same length and the same values)");
            MessageInColor("First Check:", "Green");
            PrintTwoLists(intList, intList);
            Console.WriteLine("Are two list are the same = " + RecAreListsTheSame<int>(intList, intList) + "\n");
            Enter();
            MessageInColor("Second Check:", "Green");
            PrintTwoLists(intList, copyIntList);
            Console.WriteLine("Are two list are the same = " + AreTwoListsAreTheSame<int>(intList, copyIntList) + "\n");
            Enter();
            AddAfter(copyIntList, numToCheck);
            MessageInColor("Third Check:", "Red");
            PrintTwoLists(intList, copyIntList);
            Console.WriteLine("Are two list are the same = " + RecAreListsTheSame<int>(intList, copyIntList) + "\n");
            Enter();
            DeleteAfter(copyIntList);
            DeleteAfter(copyIntList);
            AddAfter(copyIntList, 777);
            MessageInColor("Fourth Check:", "Red");
            PrintTwoLists(intList, copyIntList);
            Console.WriteLine("Are two list are the same = " + AreTwoListsAreTheSame2<int>(intList, copyIntList) + "\n");
            EnterAndClear();
            MessageInColor("\n------------------------------19----------------------------\n", "Magenta");
            Console.WriteLine("Full merging of the two lists into one list regardless of whether the value appears more than once");
            Console.WriteLine("First list = ");
            PrintList(intList);
            Console.WriteLine("Second list = ");
            PrintList(copyIntList);
            Console.WriteLine("Merge list = ");
            PrintList(MergeTwoLists<int>(intList, copyIntList));
            Enter();
            MessageInColor("\n------------------------------20----------------------------\n", "Magenta");
            Console.WriteLine("Union between two lists into one list without duplicates");
            Console.WriteLine("First list = ");
            PrintList(intList);
            Console.WriteLine("Second list = ");
            PrintList(copyIntList);
            Console.WriteLine("Merge list no duplicates = ");
            PrintList(MergeTwoListsNoDuplicates<int>(intList, copyIntList));
            Enter();
            MessageInColor("\n------------------------------21----------------------------\n", "Magenta");
            Console.WriteLine("An internal merge, which is a cut between the two lists,\nand everything that is common between the two lists without duplication");
            Console.WriteLine("Union between two lists into one list without duplicates");
            Console.WriteLine("First list = ");
            PrintList(intList);
            Console.WriteLine("Second list = ");
            PrintList(copyIntList);
            Console.WriteLine("Merge list equal values no duplicates = ");
            PrintList(MergeListsEqualValuesNoDup<int>(intList, copyIntList));
            EnterAndClear();
        }
        #endregion

        #region Check functions on WorkerList [1 - 21]
        public static void WorkerListFunctionsCheck()
        {

            Console.WriteLine("Creating worker list ...\n");

            Node<Worker> workerNode1 = new Node<Worker>(new Worker("Amit", 10000));
            Node<Worker> workerNode2 = new Node<Worker>(new Worker("Ely", 5000));
            Node<Worker> workerNode3 = new Node<Worker>(new Worker("Gal", 5000));
            Node<Worker> workerNode4 = new Node<Worker>(new Worker("Beny", 1000));

            Node<Worker> workerList = workerNode1;

            workerNode1.SetNext(workerNode2);
            workerNode2.SetNext(workerNode3);
            workerNode3.SetNext(workerNode4);
            MessageInColor("\n------------------------------1----------------------------\n", "Magenta");
            Console.WriteLine("The length of list:" + GetListLength(workerList));
            Enter();
            MessageInColor("\n------------------------------2----------------------------\n", "Magenta");
            Console.WriteLine("Print Worker list:");
            PrintList<Worker>(workerList);
            EnterAndClear();
            MessageInColor("\n------------------------------3----------------------------\n", "Magenta");
            MessageInColor("Adding a Worker Ben to the start of the list by reference:\n", "Cyan");
            PrintList(workerList);
            AddFirstByRef(ref workerList, new Worker("Ben", 19000));
            Console.WriteLine("Print new Worker list:");
            PrintList<Worker>(workerList);
            Enter();
            MessageInColor("\nAdding a Worker Gdi to the start of the list:(not by reference)\n", "Green");
            workerList = AddFirst(workerList, new Worker("Gdi", 29000));
            Console.WriteLine("Print new Worker list:");
            PrintList<Worker>(workerList);
            Enter();
            MessageInColor("\n------------------------------4----------------------------\n", "Magenta");
            Console.ForegroundColor = ConsoleColor.Cyan;
            MessageInColor("Adding a Worker Gil to the the end of the list:(regular function)\n", "Cyan");
            workerList = AddLast<Worker>(workerList, new Worker("Gil", 12000));
            Console.WriteLine("Print new Worker list:");
            PrintList<Worker>(workerList);
            Enter();
            MessageInColor("\nAdding a Worker May to the the end of the list:(recursive function)\n", "Green");
            Console.ResetColor();
            workerList = RecAddLast<Worker>(workerList, new Worker("May", 18000));
            Console.WriteLine("Print new Worker list:");
            PrintList<Worker>(workerList);
            EnterAndClear();
            MessageInColor("\n------------------------------5----------------------------\n", "Magenta");
            Console.WriteLine("Adding a Worker David to the the list after the Node we sent: (Node4 sent) \n");
            AddAfter<Worker>(workerNode2, new Worker("David", 22000));
            Console.WriteLine("Print new Worker list:");
            PrintList<Worker>(workerList);
            Enter();
            MessageInColor("\n------------------------------6----------------------------\n", "Magenta");
            Console.ResetColor(); Console.WriteLine("Delete a Worker from the start of the list\n");
            PrintList<Worker>(workerList);
            workerList = DeleteFirst<Worker>(workerList);
            Console.WriteLine("Print new Worker list:");
            PrintList<Worker>(workerList);
            Enter();
            MessageInColor("\n------------------------------7----------------------------\n", "Magenta");
            Console.WriteLine("Delete the next Node from the Node I sent (Node 2 sent)\n");
            DeleteAfter<Worker>(workerNode1);
            Console.WriteLine("Print new Worker list:");
            PrintList<Worker>(workerList);
            EnterAndClear();
            MessageInColor("\n------------------------------8----------------------------\n", "Magenta");
            MessageInColor("Delete a Worker from the end of the list:(regular function)\n", "Cyan");
            PrintList(workerList);
            workerList = DeleteLast<Worker>(workerList);
            Console.WriteLine("\nPrint new Worker list:");
            PrintList<Worker>(workerList);
            Enter();
            Console.ForegroundColor = ConsoleColor.Green;
            MessageInColor("\nDelete a Worker from the end of the list:(recursive function)\n", "Green");
            Console.ResetColor();
            workerList = RecDeleteLast<Worker>(workerList);
            Console.WriteLine("Print new Worker list:");
            PrintList<Worker>(workerList);
            EnterAndClear();
            MessageInColor("\n------------------------------9----------------------------\n", "Magenta");
            Console.WriteLine("Current list");
            PrintList<Worker>(workerList);
            Console.WriteLine("The first value of the list: " + GetFirstValue(workerList));
            Enter();
            MessageInColor("\n------------------------------10----------------------------\n", "Magenta");
            MessageInColor("The last value of the list: (regular function) = " + GetLastValue(workerList), "Cyan");
            Console.WriteLine();
            Enter();
            MessageInColor("\nThe last value of the list: (recursive function) = " + RecGetLastValue(workerList), "Green");
            Enter();
            MessageInColor("\n------------------------------11----------------------------\n", "Magenta");
            Console.WriteLine("Returning the value according to the index sent");
            PrintList<Worker>(workerList);
            Console.WriteLine();
            Console.WriteLine("The value in index 3: " + GetValueByIndex(workerList, 3));
            Console.WriteLine("The value in index 0: " + RecGetValueByIndex(workerList, 0));
            Console.WriteLine("The value in last index: " + GetValueByIndex(workerList, GetListLength(workerList) - 1));
            Enter();
            // exception
            //Console.WriteLine("The value in index -1: " + GetValueByIndex(workerList, -1));
            //Console.WriteLine("The value in index 50: " + GetValueByIndex(workerList, 50));
            //Console.WriteLine("The value in last index: " + GetValueByIndex<int>(workerList, GetListLength(intList)));
            //Enter();
            MessageInColor("\n------------------------------12----------------------------\n", "Magenta");
            MessageInColor("\n(regular function)", "Cyan");
            PrintList<Worker>(workerList);
            //Checks if it is the same object (if we want to check if there is the same name
            //and the same salary, we will override equal in the Worker class)      
            Console.WriteLine("Checking if the value exists in the array");
            Worker w1 = new Worker("Galit", 15000);
            Console.WriteLine($"Is {w1} exist in the list = {IsValueExists(workerList, w1)}\n");
            Console.WriteLine("Adding the value to the list and cheking agin");
            workerList = AddLast(workerList, w1);
            PrintList<Worker>(workerList);
            Console.WriteLine($"Is {w1} exist in the list = {IsValueExists(workerList, w1)}");
            Enter();
            MessageInColor("\n(recursive function)", "Green");
            Console.WriteLine("Checking if the value exists in the array");
            w1 = new Worker("Mor", 25000);
            Console.WriteLine($"Is {w1} exist in the list = {RecIsValueExists(workerList, w1)}\n");
            Console.WriteLine("Adding the value to the list and cheking agin");
            workerList = AddLast(workerList, w1);
            PrintList<Worker>(workerList);
            Console.WriteLine($"Is {w1} exist in the list = {RecIsValueExists(workerList, w1)}");
            EnterAndClear();
            MessageInColor("\n------------------------------13----------------------------\n", "Magenta");
            Console.WriteLine("Checking whether the list sent is a circular list\n");
            PrintList(workerList);
            Console.WriteLine("Is list circular: " + IsListCircular(workerList) + "\n");
            Console.WriteLine("Make a circular list");
            Node<Worker> workerCircularList = new Node<Worker>(new Worker("Yakov", 5000));
            workerCircularList = AddLast(workerCircularList, w1);
            workerCircularList = AddLast(workerCircularList, new Worker("Barak", 15800));
            workerCircularList.GetNext().GetNext().SetNext(workerCircularList);
            PrintCircularList(workerCircularList);
            Console.WriteLine("\nAdd Vered To the last of Circular List ");
            CircularListAddLast(workerCircularList, new Worker("Vered", 15000));
            PrintCircularList(workerCircularList);
            Console.WriteLine("\nAdd Alon to the First of Circular List ");
            workerCircularList = CircularAddFirst(workerCircularList, new Worker("Alon", 15000));
            PrintCircularList(workerCircularList);
            Console.WriteLine("\nDelete the First of Circular List ");
            workerCircularList = CircularDeleteFirst(workerCircularList);
            PrintCircularList(workerCircularList);
            Console.WriteLine("\nDelete the last of Circular List ");
            CircularDeleteLast(workerCircularList);
            PrintCircularList(workerCircularList);
            Console.WriteLine("\nIs list circular: " + IsListCircular(workerCircularList) + "\n");
            EnterAndClear();
            MessageInColor("\n------------------------------14----------------------------\n", "Magenta");
            workerList = AddLast(workerList, w1);
            Console.WriteLine("Current list");
            PrintList(workerList);
            Console.WriteLine("\nFiltering the duplicate values in the list");
            Node<Worker> noDupWorkerList = GetNewListNoDup(workerList);
            PrintList(noDupWorkerList);
            Enter();
            MessageInColor("\n------------------------------15----------------------------\n", "Magenta");
            Console.WriteLine("Returning a new list that is a duplicate of the list you sent");
            Console.WriteLine("The original list");
            PrintList(workerList);
            Node<Worker> copyWorkerList = CopyList(workerList);
            Console.WriteLine("\nThe copy list");
            PrintList(copyWorkerList);
            Enter();
            MessageInColor("\n------------------------------16----------------------------\n", "Magenta");
            MessageInColor("\n(regular function)\n", "Cyan");
            Console.WriteLine("\nTurning the list from the end to the beginning without creating a new list");
            Console.WriteLine("The original list");
            PrintList(workerList);
            Console.WriteLine("\nThe reverse list");
            workerList = ReverseOriginalList(workerList);
            PrintList(workerList);
            Enter();
            MessageInColor("\n(regular function)\n", "Green");
            Console.WriteLine("The original list");
            PrintList(workerList);
            Console.WriteLine("The reverse list");
            workerList = RecReverseOriginalList(workerList);
            PrintList(workerList);
            MessageInColor("\n------------------------------17----------------------------\n", "Magenta");
            Console.WriteLine("Sorting the list from smallest to largest");
            Console.WriteLine("The original list");
            PrintList(workerList);
            Console.WriteLine("\nThe Sorted list");
            SortList<Worker>(workerList);
            PrintList(workerList);
            EnterAndClear();
            MessageInColor("\n------------------------------18----------------------------\n", "Magenta");            
            Console.WriteLine("Are the two lists the same in terms of content (the same length and the same values)");
            MessageInColor("First Check:", "Green");
            PrintTwoLists(workerList, workerList);
            Console.WriteLine("Are two list are the same = " + AreTwoListsAreTheSame<Worker>(workerList, workerList));
            Enter();
            MessageInColor("Second Check:", "Green");
            PrintTwoLists(workerList, copyWorkerList);
            Console.WriteLine("Are two list are the same = " + RecAreListsTheSame<Worker>(workerList, copyWorkerList));
            Enter();
            AddAfter(copyWorkerList, w1);
            MessageInColor("Third Check:", "Red");
            PrintTwoLists(workerList, copyWorkerList);
            Console.WriteLine("Are two list are the same = " + AreTwoListsAreTheSame<Worker>(workerList, copyWorkerList));
            Enter();
            DeleteAfter(copyWorkerList);
            DeleteAfter(copyWorkerList);
            AddAfter(copyWorkerList, new Worker("jon", 45000));
            MessageInColor("Fourth Check:", "Red");
            PrintTwoLists(workerList, copyWorkerList);
            Console.WriteLine("Are two list are the same = " + RecAreListsTheSame<Worker>(workerList, copyWorkerList));
            EnterAndClear();
            MessageInColor("\n------------------------------19----------------------------\n", "Magenta");
            Console.WriteLine("Full merging of the two lists into one list regardless of whether the value appears more than once");
            Console.WriteLine("First list = ");
            PrintList(workerList);
            Console.WriteLine("Second list = ");
            PrintList(copyWorkerList);
            Console.WriteLine("\nMerge list = ");
            PrintList(MergeTwoLists<Worker>(workerList, copyWorkerList));
            Enter();
            MessageInColor("\n------------------------------20----------------------------\n", "Magenta");
            Console.WriteLine("Union between two lists into one list without duplicates");
            Console.WriteLine("First list = ");
            PrintList(workerList);
            Console.WriteLine("Second list = ");
            PrintList(copyWorkerList);
            Console.WriteLine("Merge list no duplicates = ");
            PrintList(MergeTwoListsNoDuplicates<Worker>(workerList, copyWorkerList));
            Enter();
            MessageInColor("\n------------------------------21----------------------------\n", "Magenta");
            Console.WriteLine("An internal merge, which is a cut between the two lists,\nand everything that is common between the two lists without duplication\n");
            Console.WriteLine("Union between two lists into one list without duplicates");
            Console.WriteLine("First list = ");
            PrintList(workerList);
            Console.WriteLine("Second list = ");
            PrintList(copyWorkerList);
            Console.WriteLine("\nMerge list equal values no duplicates = ");
            PrintList(MergeListsEqualValuesNoDup<Worker>(workerList, copyWorkerList));
            EnterAndClear();
        }
        #endregion

        #region Studens list function check
        public static void StudensListFunctionsCheck()
        {
            Node<Student> studentNode1 = new Node<Student>(new Student("Amit"));
            studentNode1.GetValue().AddCourse(new Course("01", 100));
            studentNode1.GetValue().AddCourse(new Course("02", 55));
            studentNode1.GetValue().AddCourse(new Course("03", 100));
            Node<Student> studentNode2 = new Node<Student>(new Student("Ely"));
            studentNode2.GetValue().AddCourse(new Course("01", 95));
            studentNode2.GetValue().AddCourse(new Course("06", 93));
            Node<Student> studentNode3 = new Node<Student>(new Student("Gal"));
            studentNode3.GetValue().AddCourse(new Course("01", 95));
            studentNode3.GetValue().AddCourse(new Course("05", 89));
            Node<Student> studentNode4 = new Node<Student>(new Student("Beny"));
            studentNode4.GetValue().AddCourse(new Course("01", 55));
            studentNode4.GetValue().AddCourse(new Course("02", 45));
            studentNode4.GetValue().AddCourse(new Course("03", 65));


            Node<Student> studentList1 = studentNode1;

            studentNode1.SetNext(studentNode2);
            studentNode2.SetNext(studentNode3);
            studentNode3.SetNext(studentNode4);
            Console.WriteLine("Students list\n");
            PrintSpecialList(studentList1);
            MessageInColor("\nThe average of the grades by a recursive function\n", "Magenta");
            RecPrintEachStudentGradeAverage(studentList1);
            EnterAndClear();
            //--------------------------------------------------------------------------------
            Node<Student> studentList2 = GenerateStudentsList(new string[] { "Moty", "Yael", "Vered", "Amit" }, 4);
            Console.WriteLine();
            Console.WriteLine("Students list\n");
            PrintSpecialList(studentList2);
            MessageInColor("\nThe average of the grades by a normal function \n", "Magenta");
            PrintEachStudentGradeAverage(studentList2);
        }

        public static void ClassesListFunctionsCheck()
        {

            //Q 24
            Student[] studentsClass1 = {new Student("eli", GenerateCourses(4)),
                            new Student("avi", GenerateCourses(4)),new Student("beny", GenerateCourses(4)),
                            new Student("gad", GenerateCourses(4)) };
            MessageInColor("############################################ class1 ############################################", "Green");

            PrintArrStudents(studentsClass1);
            Enter();

            Student[] studentsClass2 = { new Student("gal", GenerateCourses(5)),
                            new Student("amit", GenerateCourses(5)),new Student("shaked", GenerateCourses(5)),
                            new Student("alon", GenerateCourses(5)),new Student("elad", GenerateCourses(5)) };

            MessageInColor("############################################ class2 ############################################", "Green");


            PrintArrStudents(studentsClass2);
            Enter();

            Student[] studentsClass3 = { new Student("ido", GenerateCourses(3)),
                            new Student("dani", GenerateCourses(3)),new Student("daniel", GenerateCourses(3)),
                            new Student("berry", GenerateCourses(3)) };

            MessageInColor("############################################ class3 ############################################", "Green");


            PrintArrStudents(studentsClass3);
            Enter();

            Student[] studentsClass4 = { new Student("michal", GenerateCourses(4)),
                            new Student("koren", GenerateCourses(4)),new Student("ofek", GenerateCourses(4)),
                            new Student("yoav", GenerateCourses(4)) };

            MessageInColor("############################################ class4 ############################################", "Green");
            PrintArrStudents(studentsClass4);
            Enter();

            Node<Student[]> class4 = new Node<Student[]>(studentsClass4, null);
            Node<Student[]> class3 = new Node<Student[]>(studentsClass3, class4);
            Node<Student[]> class2 = new Node<Student[]>(studentsClass2, class3);
            Node<Student[]> class1 = new Node<Student[]>(studentsClass1, class2);

            Node<Student[]> classList = class1;
            Console.WriteLine("\nTop Students (by a recursive function)");
            PrintSpecialList(RecGetTopStudentFromList(classList));
            EnterAndClear();
            MessageInColor("############################################ class1 ############################################", "Green");
            PrintArrStudents(studentsClass1);
            Enter();
            MessageInColor("############################################ class2 ############################################", "Green");
            PrintArrStudents(studentsClass2);
            Enter();
            MessageInColor("############################################ class3 ############################################", "Green");
            PrintArrStudents(studentsClass3);
            Enter();
            MessageInColor("############################################ class4 ############################################", "Green");
            PrintArrStudents(studentsClass4);
            Enter();
            Console.WriteLine("Top Students (by a normal functionn)");
            PrintSpecialList(GetTopStudentFromList(classList));


        }

        public static void ClassesArrayFunctionsCheck()
        {


            // Q25
            Node<Student>[] classes = { GenerateStudentsList(new string[] { "Yoav", "Ben", "Amit", "Orel", "Moty" }, 5)
                                , GenerateStudentsList(new string[] { "Dani", "Ely", "Ido", "Koren", "Alon","Elad"},6)
                                , GenerateStudentsList(new string[] { "Shay", "Bob", "Hen", "Nir","Mor","Tal","Noam"},4)
                                , GenerateStudentsList(new string[] { "Barak", "Yoel", "Shaked", "Yuval", "Gil"},3)
                                , GenerateStudentsList(new string[] { "Asher", "Yona", "Avi", "Nirit", "Shmuel"},3) };

            MessageInColor("############################################ class1 ############################################", "Red");

            PrintSpecialList(classes[0]);
            Enter();
            MessageInColor("############################################ class2 ############################################", "Red");

            PrintSpecialList(classes[1]);
            Enter();
            MessageInColor("############################################ class3 ############################################", "Red");

            PrintSpecialList(classes[2]);
            Enter();
            MessageInColor("############################################ class4 ############################################", "Red");

            PrintSpecialList(classes[3]);
            Enter();
            MessageInColor("############################################ class5 ############################################", "Red");

            PrintSpecialList(classes[4]);
            Enter();

            Student[] arrFaildStudents = RecGetFailingStudents(classes);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nThe students who failed the most");
            Console.ResetColor();

            foreach (Student s in arrFaildStudents)
            {
                Console.Write(s);
            }
            Enter();
            MessageInColor("\nmax faild student:", "Green");
            Console.WriteLine(GetMaxFaildStudent(arrFaildStudents));
            EnterAndClear();
        }
        #endregion 

        #region 1. Returning the length of the list
        public static int GetListLength<T>(Node<T> list)
        {
            int counter = 0;
            while (list != null)
            {
                list = list.GetNext();
                counter++;
            }
            return counter;
        }



        //option two
        public static int RecGetListLength<T>(Node<T> list)
        {
            if (list == null) return 0;

            return 1 + RecGetListLength(list.GetNext());
        }
        #endregion.

        #region 2. Printing the list.
        public static void PrintList<T>(Node<T> list)
        {
            if (list == null)
            {
                Console.WriteLine("");
                return;
            }
            else if (!(list.HasNext()))//באיבר האחרון נירצה להדפיס נקודה במקום פסיק
                Console.Write(list.GetValue() + " -> null");
            else
                Console.Write(list.GetValue() + " -> ");
            PrintList(list.GetNext());
        }

        // print list with line drop between valuse
        public static void PrintSpecialList(Node<Student> list)
        {
            if (list == null) return;

            Console.Write(list.GetValue().DisplayStudentWithAverage() + "\n");

            PrintSpecialList(list.GetNext());
        }
        #endregion

        #region 3. Adding a value at the beginning of the list
        public static Node<T> AddFirst<T>(Node<T> list, T value)
        {
            Node<T> newNode = new Node<T>(value);//new value
            newNode.SetNext(list);// set the new node to be the head of the list

            return newNode;//list is sent by value so we need to return it or to get it by reference
        }


        public static void AddFirstByRef<T>(ref Node<T> list, T value)
        {
            Node<T> newNode = new Node<T>(value);
            newNode.SetNext(list);
            list = newNode;
        }

        #endregion.

        #region 4. Adding a value at the end of the list
        public static Node<T> AddLast<T>(Node<T> head, T value)
        {
            Node<T> newNode = new Node<T>(value);
            if (head == null)
                return newNode; //Returning a list with one Node - the Node with the value we sent

            Node<T> current = head;
            while (current.HasNext())
            {
                current = current.GetNext();
            }

            current.SetNext(newNode);
            return head;
        }

        //option two

        public static Node<T> RecAddLast<T>(Node<T> head, T value)
        {
            if (head == null) return new Node<T>(value);


            if (head.GetNext() == null)
            {
                Node<T> newNode = new Node<T>(value);
                head.SetNext(newNode);
                return head;
            }

            RecAddLast(head.GetNext(), value);
            return head;
        }
        #endregion

        #region 5. Adding a value in the middle of the list.
        public static void AddAfter<T>(Node<T> prev, T value) // 11 rest of the list -> 22 -> 34  |  10
        {
            if (prev == null) return;// Error  
            Node<T> newNode = new Node<T>(value);
            newNode.SetNext(prev.GetNext()); // 10 -> 22 -> 34
            prev.SetNext(newNode);// 11 -> 10 -> 22-> 34
        }
        #endregion

        #region 6. Deleting an Node at the beginning of the list.
        public static Node<T> DeleteFirst<T>(Node<T> list)
        {
            if (list == null) return null;
            Node<T> temp = list;
            list = list.GetNext();
            temp.SetNext(null);
            return list;
        }
        #endregion

        #region 7. Deleting a value at the end of the list.
        public static Node<T> DeleteLast<T>(Node<T> list)
        {
            if (list == null || !list.HasNext())
                return null;

            Node<T> current = list;
            while (current.GetNext().HasNext())
            {
                current = current.GetNext();
            }

            current.SetNext(null);
            return list;
        }



        //option two
        public static Node<T> RecDeleteLast<T>(Node<T> list)
        {
            if (list == null || !list.HasNext()) return null;

            if (!list.GetNext().HasNext())
            {
                list.SetNext(null);
                return list;
            }

            RecDeleteLast(list.GetNext());
            return list;
        }
        #endregion

        #region 8 Deleting a value in the middle of the list.
        public static void DeleteAfter<T>(Node<T> prev)// 1 -> 2 -> 3 rest of the list -> 4 -> 5
        {
            if (prev == null || !prev.HasNext()) return;

            Node<T> remove = prev.GetNext();// -> 4
            Node<T> next = remove.GetNext();// -> 5
            prev.SetNext(next);// 1 -> 2 -> 3 -> 5
            remove.SetNext(null);//
        }
        #endregion

        #region 9. Returning the value from the start of the list [first value]. 
        public static T GetFirstValue<T>(Node<T> list)
        {
            return list.GetValue();
        }
        #endregion

        #region 10. Returning the value at the end of the list [last value].
        public static T GetLastValue<T>(Node<T> list)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));

            while (list.HasNext())
            {
                list = list.GetNext();
            }
            return list.GetValue();
        }



        //option two
        public static T RecGetLastValue<T>(Node<T> list)
        {
            if (!list.HasNext()) return list.GetValue();

            return RecGetLastValue(list.GetNext());
        }



        //get last node 
        public static Node<T> GetLastNode<T>(Node<T> list)
        {
            if (list == null) return default;//
            while (list.HasNext())
            {
                list = list.GetNext();
            }
            return list;
        }

        //option two
        public static Node<T> RecGetLastNode<T>(Node<T> list)
        {
            if (list == null) return default;//
            if (!list.HasNext()) return list;

            return RecGetLastNode(list.GetNext());
        }
        #endregion

        #region 11. Returning the value according to the receiving index. 
        public static T GetValueByIndex<T>(Node<T> list, int index)//returning value
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "The index must be non-negative.");


            for (int i = 0; i < index; i++)
            {
                if (!list.HasNext())
                    throw new ArgumentOutOfRangeException(nameof(index),
                        "The index is greater than or equal to the length of the list.");

                list = list.GetNext();
            }

            return list.GetValue();
        }





        //option two
        public static T RecGetValueByIndex<T>(Node<T> list, int index)
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            // אם הגענו לאינקס שביקשנו נחזיר את הערך  
            if (index == 0) return list.GetValue();
            // התקדמות לאינדקס המבוקש
            return RecGetValueByIndex(list.GetNext(), index - 1);
        }



        //returnig Node
        public static Node<T> ReturnNodeByIndex<T>(Node<T> list, int index)
        {
            if (list == null || index < 0) return null;//

            for (int j = 0; j < index; j++)
            {
                if (!list.HasNext()) return null;//
                list = list.GetNext();
            }
            return list;
        }
        #endregion

        #region 12. Checking whether the sent value exists in the list or not.

        public static bool IsValueExists<T>(Node<T> list, T value)
        {
            if (list == null) return false;
            while (list != null)
            {
                if (list.GetValue().Equals(value)) return true;
                list = list.GetNext();
            }
            return false;
        }

        //option two
        public static bool RecIsValueExists<T>(Node<T> list, T value)
        {
            if (list == null) return false;
            if (list.GetValue().Equals(value)) return true;
            return RecIsValueExists(list.GetNext(), value);
        }

        #endregion

        #region 13. Checking whether the list sent is a circular list.
        public static bool IsListCircular<T>(Node<T> list)
        {
            if (list == null) return false;
            Node<T> head = list;
            while (list.HasNext())
            {
                if (list.GetNext().Equals(head)) return true;
                list = list.GetNext();
            }
            return false;
        }

        //option two
        public static bool RecIsListCircular<T>(Node<T> list)
        {
            return RecIsListCircular<T>(list, list);
        }
        public static bool RecIsListCircular<T>(Node<T> list, Node<T> head)
        {
            if (list == null || !list.HasNext()) return false;
            if (list.GetNext().Equals(head)) return true;
            return RecIsListCircular(list.GetNext(), head);
        }

        #endregion

        #region 14. Filtering the duplicate values in the list.
        public static bool Contains<T>(Node<T> list, T value)
        {
            if (list == null) return false;
            if (list.GetValue().Equals(value)) return true;
            return Contains(list.GetNext(), value);
        }

        public static Node<T> GetNewListNoDup<T>(Node<T> list)
        {
            if (list == null)
                return null;

            Node<T> newListNoDup = new Node<T>(list.GetValue());// head of the new list
            Node<T> temp = newListNoDup;
            list = list.GetNext();// secon node of list

            while (list != null)
            {
                // When new list do not contain the list value we adding the value 
                if (!Contains(newListNoDup, list.GetValue()))
                {
                    Node<T> newNode = new Node<T>(list.GetValue());
                    temp.SetNext(newNode);
                    temp = temp.GetNext();
                }
                list = list.GetNext();//Go to the next node of the list
            }

            return newListNoDup;
        }

        //option two
        public static Node<T> RecGetNewListNoDup<T>(Node<T> list)
        {
            if (list == null) return null;//
            Node<T> nextNode = list.GetNext();
            Node<T> restListNoDup = RecGetNewListNoDup(nextNode);
            //If it already contains the value we will return the new list
            //we created without adding the current value
            if (Contains(restListNoDup, list.GetValue())) return restListNoDup;
            return new Node<T>(list.GetValue(), restListNoDup);
        }




        #endregion

        #region 15. Returning a new list that is a duplicate of the list we sent.

        public static Node<T> CopyList<T>(Node<T> list)
        {
            if (list == null || !list.HasNext())
                return null;

            Node<T> newList = new Node<T>(list.GetValue());
            if (!list.HasNext()) return newList;

            Node<T> temp = newList;

            list = list.GetNext();

            while (list != null)
            {

                Node<T> newNode = new Node<T>(list.GetValue());
                temp.SetNext(newNode);
                temp = temp.GetNext();
                if (!list.HasNext()) break;
                list = list.GetNext();
            }

            return newList;
        }


        public static Node<T> RecCopyList<T>(Node<T> list)
        {
            if (list == null) return null;
            Node<T> newList = new Node<T>(list.GetValue());
            newList.SetNext(CopyList(list.GetNext()));
            return newList;
        }


        #endregion

        #region 16. Turning the list from the end to the beginning without creating a new list.

        //option one
        public static Node<T> ReverseOriginalList<T>(Node<T> list)//1 -> 2 -> 3 -> null
        {
            Node<T> current = list;//1 -> 2
            Node<T> reverseNodes = null;
            Node<T> next;

            while (current != null)
            {
                // Store the next node
                next = current.GetNext();       // 2 -> 3       |       3 -> null  |  null

                // Reverse the next pointer of the current node to point to the previous node
                current.SetNext(reverseNodes);      // 1 -> null  |    2 -> 1 -> null  |  3 -> 2 -> 1 -> null

                // Update the previous node to be the current node
                reverseNodes = current;           // 1 -> null   | 2 -> 1 -> null |3 -> 2 -> 1 -> null

                // Update the current node to be the next node
                current = next;         // 2 -> 3    |     3  -> null  |   null
            }

            return reverseNodes;     // 3 -> 2 -> 1 -> null
        }



        //option two
        public static Node<T> RecReverseOriginalList<T>(Node<T> list)
        {
            if (list == null || !list.HasNext()) return list;
            Node<T> reversedList = RecReverseOriginalList(list.GetNext());
            list.GetNext().SetNext(list);
            list.SetNext(null);
            return reversedList;
        }

        #endregion

        #region 17. Sorting the list from smallest to largest.
        // 5 -> 4 -> 9 -> 2  הרשימה שהתקבלה 
        // 2 -> 4 -> 9 -> 5 מיון ערך הקטן ביותר לתחילת הרשימה 
        // 4 -> 9 -> 5 טיפול ברשימה מהאיבר הבא 
        // 2 -> 4 -> 9 -> 5 (הרישמה לאחר הטיפול (לא קרה כלום כי זה היה כבר הנמוך ביותר במה שטופל כרגע
        // 9 -> 5  טיפול ברשימה מהאיבר הבא  
        // 5 -> 9 מה שטופל 
        // 2 -> 4 -> 5 -> 9 הרשימה לאחר המיון 

        //This function will only work with expressions whose type interfaces with IComparable
        //ComperTo is a function that return 1 if biger 0 if Equal and -1 if smaler
        public static void SortList<T>(Node<T> head) where T : IComparable<T> 
        {
           
            Node<T> current = head;// initialize current node to head of list    
            while (current != null)// loop until we reach end of list  
            {
                Node<T> minimum = current; // initialize minimum to current node   
                Node<T> temp = current.GetNext(); // initialize temp to next node   
                while (temp != null)// loop through rest of list to find minimum value
                {
                    if (temp.GetValue().CompareTo(minimum.GetValue()) < 0)// if value is smaller than minimum
                    {
                        minimum = temp;// update minimum to new value  
                    }
                    temp = temp.GetNext();// move to next node  
                }
                T tempValue = current.GetValue();// save current value to temp 
                current.SetValue(minimum.GetValue());// set current value to minimum value 
                minimum.SetValue(tempValue);// set minimum value to temp variable value 
                current = current.GetNext();// move to next node
            }
        }


        //option two

        public static void RecSortList<T>(Node<T> head) where T : IComparable<T>
        {
            if (head == null || !head.HasNext()) return;

            // Find the minimum node
            Node<T> minimum = RecFindMinimumNode(head, head);

            // Swap the head of the list with the minimum node
            T tempValue = head.GetValue();
            head.SetValue(minimum.GetValue());
            minimum.SetValue(tempValue);

            // sort the rest of the list
            RecSortList(head.GetNext());
        }

        private static Node<T> RecFindMinimumNode<T>(Node<T> current, Node<T> minimum) where T : IComparable<T>
        {
            if (current == null) return minimum;
            if (current.GetValue().CompareTo(minimum.GetValue()) < 0)
                minimum = current;
            return RecFindMinimumNode(current.GetNext(), minimum);
        }
        #endregion

        #region 18. Are Two Lists Are The Same In Terms Of Content

        public static bool AreTwoListsAreTheSame<T>(Node<T> list1, Node<T> list2) where T : IComparable<T>
        {
            if (GetListLength(list1) != GetListLength(list2)) return false;
            if (list1 == list2) return true; // if two lists are the same list

            Node<T> newList1 = CopyList(list1);
            Node<T> newList2 = CopyList(list2);

            SortList<T>(newList1);
            SortList<T>(newList2);

            while (newList1 != null)
            {
                if (!(newList1.GetValue().Equals(newList2.GetValue()))) return false;
                newList1 = newList1.GetNext();
                newList2 = newList2.GetNext();
            }
            return true;
        }

        //option two

        public static bool RecAreListsTheSame<T>(Node<T> list1, Node<T> list2) where T : IComparable<T>
        {
            if (GetListLength(list1) != GetListLength(list2)) return false;


            if (list1 == list2) return true;

            Node<T> newList1 = CopyList(list1);
            Node<T> newList2 = CopyList(list2);

            SortList(newList1);
            SortList(newList2);

            return RecAreTwoListsAreTheSame<T>(newList1, newList2);
        }

        public static bool RecAreTwoListsAreTheSame<T>(Node<T> list1, Node<T> list2)
        {

            if (!list1.GetValue().Equals(list2.GetValue())) return false;

            Node<T> nextList1 = list1.GetNext();
            Node<T> nextList2 = list2.GetNext();

            if (nextList1 == null && nextList2 == null) return true;

            return RecAreTwoListsAreTheSame(nextList1, nextList2);
        }

        //option Three
        public static bool AreTwoListsAreTheSame2<T>(Node<T> list1, Node<T> list2)
        {
            if (GetListLength(list1) != GetListLength(list2)) return false;
            if (list1 == list2) return true; // if two lists are the same list

            Node<T> newList1 = CopyList(list1);
            Node<T> newList2 = CopyList(list2);

            T valueToCheck;
            while (newList1 != null)
            {
                valueToCheck = newList1.GetValue();
                if (!(Contains(newList2, valueToCheck))) return false;
                newList2 = DeleteFirstValueOccurence(newList2, valueToCheck);
                newList1 = newList1.GetNext();
                
            }
            return true;
        }
        #endregion

        #region 19. Merge Two Lists 

        public static Node<T> MergeTwoLists<T>(Node<T> list1, Node<T> list2)
        {
            //להבנתי הקישורית לא תשפיע על הרשימות ששקיבלנו לפונקציה ולכן יצרנו
            //רשימה חדשה ע"י עותק של הרשימות שקיבלנו 
            Node<T> newList2 = CopyList(list2);
            Node<T> mergeList = CopyList(list1);
            Node<T> LastNode = GetLastNode(mergeList);
            LastNode.SetNext(newList2);

            return mergeList;
        }
        #endregion

        #region 20. Merge Two Lists without Duplicates

        public static Node<T> MergeTwoListsNoDuplicates<T>(Node<T> list1, Node<T> list2)
        {
            Node<T> mergeList = MergeTwoLists(list1, list2);
            mergeList = GetNewListNoDup(mergeList);

            return mergeList;
        }
        #endregion

        #region 21. Merge Lists Equal Values No rep 
        public static Node<T> DeleteFirstValueOccurence<T>(Node<T> head, T value)
        {
            if (head.GetValue().Equals(value)) return DeleteFirst(head);

            Node<T> temp = head;
            while (temp.HasNext())
            {
                if (temp.GetNext().GetValue().Equals(value))
                {
                    DeleteAfter(temp);
                    return head;
                }
                else temp = temp.GetNext();
            }

            return head;
        }



        public static Node<T> MergeListsEqualValuesNoDup<T>(Node<T> list1, Node<T> list2)
        {

            if (list2 is null || list1 is null) return null;

            Node<T> newList1 = GetNewListNoDup(list1);
            Node<T> newList2 = GetNewListNoDup(list2);
            Node<T> current = newList1;

            while (!(current == null))
            {
                T valueTocompare = current.GetValue();
                if (IsValueExists(newList2, valueTocompare))
                {
                    newList2 = DeleteFirstValueOccurence<T>(newList2, valueTocompare);
                    current = current.GetNext();
                }
                else
                {
                    current = current.GetNext();
                    newList1 = DeleteFirstValueOccurence<T>(newList1, valueTocompare);
                }
            }
            return newList1;
        }


        // option 2

        public static Node<T> RecDeleteFirstValueOccurence<T>(Node<T> head, T value)
        {

            if (head == null) return null;

            // if head value matches, return the rest of the list by deleting the head
            if (head.GetValue().Equals(value)) return DeleteFirst(head);

            // check the rest of the list
            head.SetNext(RecDeleteFirstValueOccurence(head.GetNext(), value));

            // return the updated head
            return head;
        }


        public static Node<T> MergeListsEqualValues<T>(Node<T> list1, Node<T> list2)
        {
            if (list1 == null || list2 == null) return null;

            Node<T> newList1 = GetNewListNoDup(list1);
            Node<T> newList2 = GetNewListNoDup(list2);

            return RecMergeEqualValuesNoDup(newList1, newList2);
        }

        private static Node<T> RecMergeEqualValuesNoDup<T>(Node<T> list1, Node<T> list2)
        {
            if (list1 == null) return null;

            if (IsValueExists(list2, list1.GetValue()))
            {
                list2 = DeleteFirstValueOccurence(list2, list1.GetValue());
                list1.SetNext(RecMergeEqualValuesNoDup(list1.GetNext(), list2));
                return list1;
            }

            list1 = DeleteFirstValueOccurence(list1, list1.GetValue());
            return RecMergeEqualValuesNoDup(list1, list2);
        }
        #endregion

        #region 23. Print Each Student Average Grade

        //option 1
        public static int GetAverage(Node<Course> listOfCourses)
        {
            int sumGrades = 0;
            int countCourses = 0;

            while (listOfCourses != null)
            {
                sumGrades += listOfCourses.GetValue().GetGrade();
                countCourses++;
                if (listOfCourses == null) break;
                listOfCourses = listOfCourses.GetNext();
            }
            int averageGrade = sumGrades / countCourses;
            return averageGrade;
        }

        public static void PrintEachStudentGradeAverage(Node<Student> studentList)
        {

            Node<Course> listOfCourses;
            Student student;
            while (studentList != null)
            {
                student = studentList.GetValue();
                listOfCourses = student.GetCoursesList();
                string studentName = student.GetName();
                int averageGrade = GetAverage(listOfCourses);
                Console.WriteLine($"The average grade of {studentName} is {averageGrade}.");
                if (studentList == null) break;
                studentList = studentList.GetNext();
            }
        }


        //option 2

        private static void RecPrintEachStudentGradeAverage(Node<Student> current)
        {
            if (current == null) return;
            Student currentStudent = current.GetValue();
            string studentName = currentStudent.GetName();
            int averageGrade = currentStudent.GetAverageGrade(); // uses  class student method to get Average Grade
            Console.WriteLine($"{studentName} average grade is {averageGrade}.");
            RecPrintEachStudentGradeAverage(current.GetNext());
        }


        #endregion

        #region 24.Returns a list of the top students 
        public static Node<Student> GetTopStudentFromList(Node<Student[]> classes)
        {
            Node<Student> topStudents = null;
            while (classes != null)
            {
                int maxGrade = 0;
                Student maxStudent = null;
                foreach (Student student in classes.GetValue())
                {
                    int average = GetAverage(student.GetCoursesList());
                    if (maxGrade < average)
                    {
                        maxGrade = average;
                        maxStudent = student;
                    }
                }
                topStudents = AddFirst<Student>(topStudents, maxStudent);
                classes = classes.GetNext();
            }
            return topStudents;
        }


        // option 2

        public static Node<Student> RecGetTopStudentFromList(Node<Student[]> classes)
        {
            if (classes == null) return null;

            Student topStudent = null;
            int topGrade = 0;
            foreach (Student student in classes.GetValue())
            {
                int grade = student.GetAverageGrade();
                if (grade > topGrade)
                {
                    topGrade = grade;
                    topStudent = student;
                }
            }
            return AddFirst(RecGetTopStudentFromList(classes.GetNext()), topStudent);
        }

        #endregion

        #region 25.Return the most failing students

        public static int GetAmountOfFailedCourses(Node<Course> listOfCourses)
        {
            int countFailCourses = 0;
            const int PASSING_GRADE = 56;
            while (listOfCourses != null)
            {
                int grade = listOfCourses.GetValue().GetGrade();
                if (grade < PASSING_GRADE) countFailCourses++;
                if (listOfCourses == null) break;
                listOfCourses = listOfCourses.GetNext();
            }

            return countFailCourses;
        }

        public static Student GetFaildStudent(Node<Student> studentList)
        {

            Node<Course> listOfCourses;
            Student student = null;
            int maxFailStudent = 0;
            while (studentList != null)
            {

                listOfCourses = studentList.GetValue().GetCoursesList();
                int amountOfFailedCourses = GetAmountOfFailedCourses(listOfCourses);
                if (amountOfFailedCourses > maxFailStudent)
                {
                    maxFailStudent = amountOfFailedCourses;
                    student = studentList.GetValue();
                }
                if (studentList == null) break;
                studentList = studentList.GetNext();
            }
            return student;
        }
        public static Student[] GetMostFailingStudents(Node<Student>[] classArr)
        {
            Student[] failngArr = new Student[classArr.Length];
            int index = 0;
            foreach (Node<Student> Classroom in classArr)
            {
                failngArr[index++] = GetFaildStudent(Classroom);
            }
            return failngArr;
        }


        // option 2

        public static Student RecGetMaxFaildStudent(Node<Student> studentList, Student maxFailStudent = null)
        {
            if (studentList == null) return maxFailStudent;

            Student currentStudent = studentList.GetValue();

            //uses a method from class Courses GetAmountOfFailedCourses
            if (maxFailStudent == null ||
                currentStudent.GetAmountOfFailedCourses() > maxFailStudent.GetAmountOfFailedCourses())
            { maxFailStudent = currentStudent; }

            return RecGetMaxFaildStudent(studentList.GetNext(), maxFailStudent);
        }

        public static Student[] RecGetFailingStudents(Node<Student>[] classArr)//creatig array with the faling students
        {
            if (classArr == null) return null;

            Student[] failngArr = new Student[classArr.Length];
            for (int i = 0; i < classArr.Length; i++)
            {
                failngArr[i] = RecGetMaxFaildStudent(classArr[i]);
            }

            return failngArr;
        }


        #endregion


        #region circularList Functions
        public static void CircularDeleteLast<T>(Node<T> list)
        {
            if (list == null || !list.HasNext())
                return;
            Node<T> head = list;
            Node<T> current = list;
            while (true)
            {
                if (list.GetNext().GetNext().Equals(head))
                {
                    list.SetNext(head);
                    break;
                };
                // if last node (next equals to the head return the node)
                list = list.GetNext();// if not last keep going
            }
        }

        public static Node<T> CircularDeleteFirst<T>(Node<T> list)
        {
            if (list == null) return null;
            Node<T> tail = GetCircularListLastNode(list);
            Node<T> temp = list;
            list = list.GetNext();
            tail.SetNext(list);
            temp.SetNext(null);
            return list;
        }

        public static Node<T> CircularAddFirst<T>(Node<T> list, T value)
        {
            Node<T> tail = GetCircularListLastNode(list);
            Node<T> newHead = new Node<T>(value);
            newHead.SetNext(list);
            tail.SetNext(newHead);
            return newHead;
        }

        //A function that returns the length of the circular list.
        //If the length is less than 1, it means that it is not circular
        public static int IsCircularListGetLength<T>(Node<T> list)
        {
            if (list == null) return 0;
            int count = 0;
            Node<T> head = list;
            while (list.HasNext())
            {
                count++;
                if (list.GetNext().Equals(head)) return count;
                list = list.GetNext();
            }
            return -1;
        }
        public static Node<T> GetCircularListLastNode<T>(Node<T> list)
        {
            if (list == null) return null;
            Node<T> head = list;
            while (list.HasNext())
            {
                if (list.GetNext().Equals(head)) return list; // if last node (next equals to the head return the node)
                list = list.GetNext();// if not last keep going
            }
            throw new Exception(nameof(list)); //if not circular
        }
        public static void CircularListAddLast2<T>(Node<T> list, T value)
        {
            Node<T> lastNode = GetCircularListLastNode(list);
            Node<T> newNode = new Node<T>(value);
            newNode.SetNext(list); // or - newNode.SetNext(lastNode.GetNext());
            lastNode.SetNext(newNode);
        }

        public static void CircularListAddLast<T>(Node<T> list, T value)
        {
            if (list == null) return;
            Node<T> head = list;
            Node<T> newNode = new Node<T>(value);

            while (true)
            {
                if (list.GetNext().Equals(head))
                {
                    newNode.SetNext(head);
                    list.SetNext(newNode);
                    return;
                }
                list = list.GetNext();
            }
        }
        public static void PrintCircularList<T>(Node<T> list)
        {

            if (list == null) return;
            Node<T> head = list;
            int sum = 0;
            while (true)
            {
                string showElement = list.GetValue() + " -> ";
                sum += showElement.Length;
                if (list.GetNext().Equals(head)) { Console.WriteLine(list.GetValue()); break; }
                Console.Write(showElement);
                list = list.GetNext();
            }
            Console.Write("↑");
            Console.ForegroundColor = ConsoleColor.Yellow;
            for (int i = 0; i < sum - 7; i++)
            {
                Console.Write("-");
            }
            Console.ResetColor();
            Console.WriteLine("↓");
        }
        #endregion

        public static void PrintTwoLists<T>(Node<T> list1, Node<T> list2)
        {
            Console.WriteLine("First list = ");
            PrintList(list1);
            Console.WriteLine("Second list = ");
            PrintList(list2);
        }

        public static Node<Course> GenerateCourses(int numOfCourses)
        {

            Random random = new Random(Guid.NewGuid().GetHashCode());//
            Course c = new Course($"00{1}", random.Next(40, 101));
            Node<Course> listCourses = new Node<Course>(c);

            for (int i = 2; i < numOfCourses + 1; i++)
            {
                c = new Course($"00{i}", random.Next(40, 101));
                listCourses = AddLast(listCourses, c);
            }

            //PrintList(listCourses);
            return listCourses;
        }

        public static void PrintArrStudents(Student[] studentsClass)
        {
            foreach (Student student in studentsClass)
            {
                Console.Write(student);
            }
        }

        public static Node<Student> GenerateStudentsList(string[] namesArr, int numOfCourses)
        {
            Student s = new Student(namesArr[0], GenerateCourses(numOfCourses));
            Node<Student> listStudents = new Node<Student>(s);

            for (int i = 1; i < namesArr.Length; i++)
            {
                s = new Student(namesArr[i], GenerateCourses(numOfCourses));
                listStudents = AddLast(listStudents, s);
            }

            //PrintList(listCourses);
            return listStudents;
        }

        public static Student GetMaxFaildStudent(Student[] arrFaild)
        {
            Student maxFaildStudent = arrFaild[0];
            int amountOfCourses = GetListLength(maxFaildStudent.GetCoursesList());
            double ratio = maxFaildStudent.GetAmountOfFailedCourses() / (double)amountOfCourses;
            Student s;
            for (int i = 1; i < arrFaild.Length; i++)
            {
                s = arrFaild[i];
                amountOfCourses = GetListLength(s.GetCoursesList());
                double numFaild = s.GetAmountOfFailedCourses() / (double)amountOfCourses;
                //Console.WriteLine(numFaild);
                if (ratio < numFaild)
                {
                    ratio = numFaild;
                    maxFaildStudent = s;
                }
            }
            return maxFaildStudent;
        }

        public static void MessageInColor(string msg,string color)
        {
            switch (color)
            {
                case "Magenta":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "Cyan":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "Green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "Red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        public static void MakeListCircular<T>(Node<T> list)
        {
            if (IsListCircular(list))
                return;
            Node<T> tail = GetLastNode(list);
            tail.SetNext(list);
            PrintCircularList(list);
        }

        public static void MakeListNotCircular<T>(Node<T> list)
        {
            if (!IsListCircular(list))
                return;
            Node<T> tail = GetCircularListLastNode(list);
            tail.SetNext(null);
            PrintList(list);
        }

        public static void EnterAndClear()
        {
            Console.WriteLine("\nEnter to continue.");
            Console.ReadKey();
            Console.Clear();
        }

        public static void Enter()
        {
            Console.WriteLine("\nEnter to continue.");
            Console.ReadKey();
        }

        public static Node<int> CreateIntList(Node<int> intList)
        {
            while (true)
            {
                string number = "";
                try
                {
                    Console.Write("Enter a number or enter exit to go back: ");
                    number = Console.ReadLine();
                    if (number.Equals("exit")) break;
                    intList = AddLast(intList, int.Parse(number));
                }
                catch (Exception)
                {
                    Console.Write("Input was invalid ,try again.\n");
                    return CreateIntList(intList);
                }

            }
            return intList;
        }

        public static Node<Worker> CreateWorkerList(Node<Worker> workerList)
        {
            while (true)
            {
                try
                {
                    Console.Write("Enter Name: or enter exit to go back:");
                    string name = Console.ReadLine();
                    if (name.Equals("exit")) break;
                    Console.Write("Enter Salry: ");
                    double salary = double.Parse(Console.ReadLine());
                    workerList = AddLast(workerList, new Worker(name, salary));
                }
                catch (Exception)
                {
                    Console.Write("Input was invalid ,try again.\n");
                    return CreateWorkerList(workerList);
                }
            }
            return workerList;
        }

        static T GetNewWorkerFromUser<T>()//Create a new worker ,using the user input [name and salary] and return it as T type
        {
            try
            {
                Console.Write("Enter Name: ");
                string name = Console.ReadLine();
                Console.Write("Enter Salary: ");
                double salary = double.Parse(Console.ReadLine());
                return (T)(object)new Worker(name, salary);//creates a new worker and cast it to object than T since all objects can be casting to T and worker cant

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nInput was invalid ,try again.\n");
                return GetNewWorkerFromUser<T>();
            }
        }

        static T GetIntFromUser<T>()//Gets a number from the user and cast it into T and return it. if there is a exception call the func again
        {
            try
            {
                Console.Write("Enter a number: ");
                int numInt = int.Parse(Console.ReadLine());
                return (T)(object)numInt;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\nInput was invalid ,try again.\n");
                return GetIntFromUser<T>();
            }

        }

        static int InputChoice(string msg)//Receiving and filtering string input
        {
            try
            {
                Console.Write(msg);
                int choice = int.Parse(Console.ReadLine());
                return choice;

            }
            catch (Exception)
            {
                Console.WriteLine("\nInput was invalid ,try again.\n");
                return InputChoice(msg);
            }
        }

    }
}

