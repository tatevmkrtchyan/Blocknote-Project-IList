using System;
using System.Collections;
using System.Collections.Generic;

namespace Blocknote_IList
{
    #region Record
    class Record
    {
        public String Name { get; private set; }
        public String Phone { get; private set; }

        public Record(string name, string phone)
        {
            this.Name = name;
            this.Phone = phone;
        }

        public Record()
        {
            this.Name = "Unknown";
            this.Phone = "";
        }

        public override bool Equals(object o)
        {
            Record record = (Record)o;
            return (record.Name == this.Name &&
                     record.Phone == this.Phone);
        }

        public override string ToString()
        {
            return String.Format($"Name` {this.Name}, Phone` {this.Phone}");
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Phone.GetHashCode();
        }
    }
    #endregion

    class Bloknot : IList<Record>
    {
        List<Record> ListOfRecords;

        public Bloknot()
        {
            ListOfRecords = new List<Record>();
        }

        public Record[] Find(String name)
        {
            int count = 0;

            for (int i = 0; i < ListOfRecords.Count; i++)
            {
                if (ListOfRecords[i].Name == name)
                    count++;
            }

            Record[] ArrayRec = new Record[count];
            int j = 0;
            for (int i = 0; i < ListOfRecords.Count; i++)
            {
                if (ListOfRecords[i].Name == name)
                {
                    ArrayRec[j++] = ListOfRecords[i];
                }
            }
            return ArrayRec;
        }

        public void Display()
        {
            for (int i = 0; i < ListOfRecords.Count; i++)
            {
                Console.WriteLine(ListOfRecords[i].ToString());
            }
            Console.WriteLine();
        }
        #region 

        public Record this[int index]
        {
            get
            {
                return ListOfRecords[index];
            }

            set
            {
                ListOfRecords[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return ListOfRecords.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Add(Record item)
        {
            ListOfRecords.Add(item);
        }

        public void Clear()
        {
            ListOfRecords.Clear();
        }

        public bool Contains(Record item)
        {
            return ListOfRecords.Contains(item);
        }

        public void CopyTo(Record[] array, int arrayIndex)
        {
            ListOfRecords.CopyTo(array, arrayIndex);
        }

        public IEnumerator<Record> GetEnumerator()
        {
            foreach (Record record in ListOfRecords)
            {
                yield return record;
            }
        }

        public int IndexOf(Record item)
        {
            return ListOfRecords.IndexOf(item);
        }

        public void Insert(int index, Record item)
        {
            ListOfRecords.Insert(index, item);
        }

        public bool Remove(Record item)
        {
            if (ListOfRecords.Remove(item))
                return true;
            else
                return false;
        }

        public void RemoveAt(int index)
        {
            ListOfRecords.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        #endregion
    }

    static class Extentions
    {
        public static IEnumerable<Record> Search(this IEnumerable<Record> array, Func<Record, bool> predicate)
        {
            foreach (Record item in array)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static void Action(this IEnumerable<Record> array, Action<Record> action)
        {
            foreach (Record item in array)
            {
                action(item);
            }
        }
    }



    class Program
    {
        static bool FindByName(Record rec)
        {
            if (rec.Name == "Anna")
                return true;
            else
                return false;
        }

        static void Main(string[] args)
        {
            Bloknot MyBloknot = new Bloknot();
            MyBloknot.Add(new Record("Anna", "224652"));
            MyBloknot.Add(new Record("Mane", "125485"));
            MyBloknot.Add(new Record("Tatev", "1254696"));
            MyBloknot.Add(new Record("Anahit", "12547852"));
            MyBloknot.Add(new Record("Anna", "1546852"));

            Func<Record, bool> del = FindByName;

            MyBloknot.Search(del).Action(Console.WriteLine);
        }
    }

}

