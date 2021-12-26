
using System.Collections;


namespace aaaa
{
    public class Dict<TK, TV> : IDictionary<TK, TV>
    {
        public ICollection<TK> Keys { get; }
        public ICollection<TV> Values { get; }

        private LinkedList<KeyValuePair<TK, TV>>[] hashtable;
     
        private int count = 0;
        public int Count => count;
        public bool IsReadOnly { get; }


        public Dict()
        {
            Keys = new List<TK>();
            Values = new List<TV>();
            IsReadOnly = false;
            hashtable = new LinkedList<KeyValuePair<TK, TV>>[16];
            for (int i = 0; i < hashtable.Length; i++)
                hashtable[i] = new LinkedList<KeyValuePair<TK, TV>>();
        }
       


        public void Add(TK key, TV value)
        {
            int hashTa = Math.Abs(key.GetHashCode()) % hashtable.Length;
            if (!Keys.Contains(key))
            {
                KeyValuePair<TK, TV> pairEl = new KeyValuePair<TK, TV>(key, value);
                hashtable[hashTa].AddLast(pairEl);
                count++;
                Keys.Add(key);
                Values.Add(value);

                if (count >= hashtable.Length)
                {
                    extendD();
                }
            }
            else
            {
                throw new ArgumentException("key already exists");
            }
        }


        private void extendD()
        {
            LinkedList<KeyValuePair<TK, TV>>[] hashtable1 = hashtable;
            hashtable = new LinkedList<KeyValuePair<TK, TV>>[hashtable1.Length * 2];

            foreach (var linkedList in hashtable1)
            {
                if (linkedList.Count != 0)
                    foreach (var item in linkedList)
                        Add(item);
            }
        }


        public bool Remove(TK key)
        {
            int hashT = Math.Abs(key.GetHashCode()) % hashtable.Length;
            if (ContainsKey(key))
            {
                foreach (var item in hashtable[hashT])
                {
                    if (key.Equals(item.Key))
                    {
                        count--;
                        hashtable[hashT].Remove(item);
                        Keys.Remove(key);
                        Values.Remove(item.Value);
                        return true;
                    }
                }
            }
            return false;
        }


        public void CopyTo(KeyValuePair<TK, TV>[] array, int arrayIndex)
        {
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("Index < 0");
            if (array == null)
                throw new ArgumentNullException("Array = null");
            if (count > array.Length - 1 - arrayIndex)
                throw new ArgumentException("Too many elements" );
            for (int i = 0; i < hashtable.Length; ++i)
            {
                foreach (var item in hashtable[i])
                {
                    if (arrayIndex >= array.Length) break;
                    array[arrayIndex] = item;
                    arrayIndex++;
                }
            }
        }


        public bool TryGetValue(TK key, out TV value)
        {
            int hashT = Math.Abs(key.GetHashCode()) % hashtable.Length;
            if (ContainsKey(key))
            {
                foreach (var item in hashtable[hashT])
                {
                    value = item.Value;
                    return true;
                }
            }
            value = default;
            return false;
        }


        public void Clear()
        {
            for (int i = 0; i < hashtable.Length; i++)
                hashtable[i].Clear();
            Keys.Clear();
            Values.Clear();
            count = 0;
        }


        public bool ContainsKey(TK key)
        {
            return Keys.Contains(key);
        }


        public bool Contains(KeyValuePair<TK, TV> Pair)
        {
            int hashT = Math.Abs(Pair.Key.GetHashCode()) % hashtable.Length;
            return hashtable[hashT].Contains(Pair);
        }


        public void Add(KeyValuePair<TK, TV> Pair)
        {
            Add(Pair.Key, Pair.Value);
        }

        

        public bool Remove(KeyValuePair<TK, TV> keyValuePair)
        {
            return Remove(keyValuePair.Key);
        }
       
        
        public TV this[TK key]
        {
            get
            {
                int hashT = Math.Abs(key.GetHashCode()) % hashtable.Length;

                if (Keys.Contains(key))
                {

                    foreach (var item in hashtable[hashT])
                    {
                        if (key.Equals(item.Key))
                        {
                            return item.Value;
                        }
                    }
                    return default;
                }
                else
                {
                    throw new KeyNotFoundException("Not found");
                }
            }
            set
            {
                int hashT = Math.Abs(key.GetHashCode()) % hashtable.Length;
                if (Keys.Contains(key))
                {
                    KeyValuePair<TK, TV> newPair = new KeyValuePair<TK, TV>(key, value);
                    foreach (var item in hashtable[hashT])
                    {
                        if (key.Equals(item.Key))
                        {
                            hashtable[hashT].Remove(item);
                            hashtable[hashT].AddLast(newPair);
                            break;
                        }
                    }
                }
                else Add(key, value);

            }
        }
        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            for (int i = 0; i < hashtable.Length; ++i)
            {
                foreach (var item in hashtable[i])
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
