using System;
using DiskLib;

namespace DiskShop
{
    class ShopApp
    {
        static InfoManager Info;
        static Storage DiskStorage;
        static void Main(string[] args)
        {
            Info = new InfoManager();
            DiskStorage = new Storage();
            bool alive = true;
            while (alive)
            {
                ColorPrint(ConsoleColor.DarkGreen, "Choose access mode:\n" +
                    "1 = Info; 2 = Storage; 3 = Buy; 0 to exit;");
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 0:
                            alive = false;
                            continue;
                        case 1://Info manager-----------
                            ItemType type = ItemType.undefined;
                            ColorPrint(ConsoleColor.DarkBlue, "What are you interested in?\n" +
                                "1 = Disks; 2 = Authors; 3 = Publishers; 4 = Genres;");
                            while (type == ItemType.undefined)
                            {
                                try
                                {
                                    switch (Convert.ToInt32(Console.ReadLine()))
                                    {
                                        case 1:
                                            type = ItemType.disk;
                                            break;
                                        case 2:
                                            type = ItemType.author;
                                            break;
                                        case 3:
                                            type = ItemType.publisher;
                                            break;
                                        case 4:
                                            type = ItemType.genre;
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                                }
                            }
                            while (type != ItemType.undefined)
                            {
                                ColorPrint(ConsoleColor.DarkMagenta, "What do you want?\n" +
                                    "1 = List all; 2 = Add; 3 = Show one (+modify or delete); 0 = Bring me back;");
                                try
                                {
                                    switch (Convert.ToInt32(Console.ReadLine()))
                                    {
                                        case 0:
                                            type = ItemType.undefined;
                                            continue;
                                        case 1:
                                            if (Info.items[type].Count == 0)
                                            {
                                                throw new Exception("No items of this category.");
                                            }
                                            foreach (InfoItem item in Info.items[type])
                                            {
                                                Console.WriteLine(item.GetInfo());
                                            }
                                            break;
                                        case 2:
                                            InfoItem tempI = Construct(type);
                                            if (tempI != null)
                                            {
                                                Info.Add(tempI, CreateInfoHandler, ModifyInfoHandler, DestroyInfoHandler);
                                            }
                                            else
                                            {
                                                throw new Exception("Failed to make one.");
                                            }
                                            break;
                                        case 3:
                                            if (Info.items[type].Count == 0)
                                            {
                                                throw new Exception("No items of this category.");
                                            }
                                            ShowOneF(type);
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                                }
                            }
                            break;
                        case 2://Storage manager-----------(stock take list show...)
                            
                            bool Triggered = true;
                            while (Triggered)
                            {
                                ColorPrint(ConsoleColor.DarkMagenta, "Disk storage: What do you want to do?\n" +
                                    "1 = List all; 2 = Add; 3 = Show one (+modify); 0 = Bring me back;");
                                try
                                {
                                    switch (Convert.ToInt32(Console.ReadLine()))
                                    {
                                        case 0:
                                            Triggered = false;
                                            continue;
                                        case 1:
                                            if (DiskStorage.Items.Count == 0)
                                            {
                                                throw new Exception("No items registered in storage.");
                                            }
                                            foreach (System.Collections.Generic.KeyValuePair<int, int> item in DiskStorage.Items)
                                            {
                                                Console.WriteLine($"id:{item.Key}: " +
                                                    $"{((DiskInfo)Info.GetByID(ItemType.disk, item.Key)).Name}, {item.Value} in stock.");
                                            }
                                            break;
                                        case 2:
                                            ColorPrint(ConsoleColor.DarkYellow, "Enter fields like this:\n" +
                                                 "id amount");
                                            try
                                            {
                                                string[] wow = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                                if (wow.Length == 2)
                                                {
                                                    int NId = Convert.ToInt32(wow[0]);
                                                    int Val = Convert.ToInt32(wow[1]);
                                                    if (Info.GetByID(ItemType.disk, NId) == null)
                                                    {
                                                        throw new Exception("No such disk detected.");
                                                    }
                                                    if (Val < 1)
                                                    {
                                                        throw new Exception("Amount must be at least 1.");
                                                    }
                                                    DiskStorage.Stock(NId, Val);
                                                }
                                                else
                                                {
                                                    throw new Exception("Wrong amount of arguments.");
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                                            }
                                            break;
                                        case 3:
                                            if (DiskStorage.Items.Count == 0)
                                            {
                                                throw new Exception("No disks registered.");
                                            }
                                            ColorPrint(ConsoleColor.DarkYellow, "Id of the disk you want to see:");
                                            try
                                            {
                                                string[] wow = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                                                int NId;
                                                if (wow.Length == 1)
                                                {
                                                    NId = Convert.ToInt32(wow[0]);
                                                    if (Info.GetByID(ItemType.disk, NId) == null)
                                                    {
                                                        throw new Exception("No such disk detected.");
                                                    }
                                                    Console.WriteLine($"We have {DiskStorage.GetAmount(NId)} of those.");
                                                }
                                                else
                                                {
                                                    throw new Exception("Wrong amount of arguments.");
                                                }
                                                ColorPrint(ConsoleColor.DarkCyan, $"Enter a number to add/subtract.");
                                                try
                                                {
                                                    int TempInt = Convert.ToInt32(Console.ReadLine());
                                                    if (TempInt < 0)
                                                    {
                                                        DiskStorage.Take(NId, Math.Abs(TempInt));
                                                    }
                                                    else if(TempInt > 0)
                                                    {
                                                        DiskStorage.Stock(NId, TempInt);
                                                    }
                                                    Console.WriteLine($"Now we have { DiskStorage.GetAmount(NId)} of those.");
                                                }
                                                catch (Exception e)
                                                {
                                                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                                                }

                                            }
                                            catch (Exception e)
                                            {
                                                ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                                            }
                                            break;
                                    }
                                }
                                catch (Exception e)
                                {
                                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                                }
                            }
                            break;
                        case 3://Buy--------(list showbyid get)
                            bool BuyLoop = true;
                            while (BuyLoop)
                            {
                                try
                                {
                                    if (DiskStorage.Items.Count == 0)
                                    {
                                        BuyLoop = false;
                                        throw new Exception("No items registered in storage.");
                                    }
                                    foreach (System.Collections.Generic.KeyValuePair<int, int> item in DiskStorage.Items)
                                    {
                                        Console.WriteLine($"id:{item.Key}: " +
                                            $"{((DiskInfo)Info.GetByID(ItemType.disk, item.Key)).Name}, {item.Value} in stock.");
                                    }
                                    ColorPrint(ConsoleColor.DarkYellow, "Id of the disk you want to buy (Enter 0 to quit):");
                                    int TempInt = Convert.ToInt32(Console.ReadLine());
                                    if (TempInt == 0)
                                    {
                                        BuyLoop = false;
                                        continue;
                                    }
                                    DiskStorage.Take(TempInt, 1);
                                    Console.WriteLine($"Bought 1 '{Info.GetByID(ItemType.disk, TempInt).GetName()}'.");

                                }
                                catch (Exception e)
                                {
                                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                                }
                            }
                            break;
                            
                    }
                }
                catch (Exception e)
                {
                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                }
            }
        }
        static void ColorPrint(ConsoleColor fcolor, string text)
        {
            ConsoleColor stdcolor = Console.ForegroundColor;
            Console.ForegroundColor = fcolor;
            Console.WriteLine(text);
            Console.ForegroundColor = stdcolor;
        }
        static InfoItem Construct(ItemType type)
        {
            InfoItem newI = null;
            bool loop = true;
            switch (type)
            {
                case ItemType.author:
                    ColorPrint(ConsoleColor.DarkYellow, "Enter fields like this:\n" +
                        "FirstName SecondName d/m/y");
                    while (loop)
                    {
                        try
                        {
                            newI = new AuthorInfo(Console.ReadLine());
                            loop = false;
                        }
                        catch (Exception e)
                        {
                            ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                        }
                    }
                    break;
                case ItemType.disk:
                    ColorPrint(ConsoleColor.DarkYellow, "Enter fields like this:\n" +
                        "Name AuthorID PublisherID d/m/y genreId1 (genreId2...)");
                    while (loop)
                    {
                        try
                        {
                            newI = new DiskInfo(Console.ReadLine());
                            loop = false;
                        }
                        catch (Exception e)
                        {
                            ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                        }
                    }
                    break;
                case ItemType.genre:
                    ColorPrint(ConsoleColor.DarkYellow, "Enter fields like this:\n" +
                        "Name");
                    while (loop)
                    {
                        try
                        {
                            newI = new GenreInfo(Console.ReadLine());
                            loop = false;
                        }
                        catch (Exception e)
                        {
                            ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                        }
                    }
                    break;
                case ItemType.publisher:
                    ColorPrint(ConsoleColor.DarkYellow, "Enter fields like this:\n" +
                        "Name");
                    while (loop)
                    {
                        try
                        {
                            newI = new PublisherInfo(Console.ReadLine());
                            loop = false;
                        }
                        catch (Exception e)
                        {
                            ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                        }
                    }
                    break;
            }
            return newI;
        }
        private static void ShowOneF(ItemType type)
        {
            InfoItem SelI = null;
            bool loop = true;
            int TempInt;
            //choosing an item
            while (loop)
            {
                ColorPrint(ConsoleColor.DarkCyan, $"Enter id of the needed {type} or 0 to exit.");
                try
                {
                    TempInt = Convert.ToInt32(Console.ReadLine());
                    if (TempInt == 0)
                    {
                        loop = false;
                        continue;
                    }
                    SelI = Info.GetByID(type, TempInt);
                    if (SelI == null)
                    {
                        throw new Exception("No such element listed.");
                    }
                    else
                    {
                        loop = false;
                    }
                }
                catch (Exception e)
                {
                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                }
            }
            ColorPrint(ConsoleColor.Gray, SelI.GetInfo());
            loop = true;
            while (loop)
            {
                ColorPrint(ConsoleColor.DarkCyan, $"Wanna do something?\n" +
                    $"1 - delete; 2 - edit; 0 - no;");
                try
                {
                    switch (Convert.ToInt32(Console.ReadLine()))
                    {
                        case 0:
                            loop = false;
                            continue;
                        case 1:
                            Info.Remove(type, SelI.Id);
                            break;
                        case 2:
                            bool sloop = true;
                            while (sloop)
                            {
                                switch (type)
                                {
                                    case ItemType.author:
                                        ColorPrint(ConsoleColor.DarkCyan, "1-fist name; 2-second name; 3-birth day; 0-return;");
                                        break;
                                    case ItemType.publisher:
                                    case ItemType.genre:
                                        ColorPrint(ConsoleColor.DarkCyan, "1-name; 0-return;");
                                        break;
                                    case ItemType.disk:
                                        ColorPrint(ConsoleColor.DarkCyan, "1-name; 2-authorId; 3-publisherId;" +
                                            " 4-release date; 5-genre; 0-return;");
                                        break;
                                }
                                try
                                {
                                    TempInt = Convert.ToInt32(Console.ReadLine());
                                    if (TempInt == 0)
                                    {
                                        sloop = false;
                                        continue;
                                    }
                                    if ((TempInt == 5) && (type == ItemType.disk))
                                    {
                                        ColorPrint(ConsoleColor.DarkCyan, "Write genre id to add in list or -1 to delete all.");
                                    }
                                    else
                                    {
                                        ColorPrint(ConsoleColor.DarkCyan, "Write new value.");
                                    }
                                    Info.Modify(type, SelI.Id, TempInt, Console.ReadLine());
                                }
                                catch (Exception e)
                                {
                                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                                }
                            }
                            break;
                    }
                    
                }
                catch (Exception e)
                {
                    ColorPrint(ConsoleColor.Red, $"Error: {e.Message}");
                }
            }
        }
        private static void CreateInfoHandler(object sender, string message)
        {
            ColorPrint(ConsoleColor.DarkGray, message);
        }
        private static void ModifyInfoHandler(object sender, string message)
        {
            ColorPrint(ConsoleColor.DarkGray, message);
        }
        private static void DestroyInfoHandler(object sender, string message)
        {
            ColorPrint(ConsoleColor.DarkGray, message);
        }
    }
}
