using System.Collections;



namespace Kt.Kernel;


public class ServiceDetectors : IEnumerable, IEnumerator, IDisposable {
    protected class ServiceDetectorItem {
        public ServiceDetectorItem Next;
        public ServiceDetectorItem Prev;
        public ServiceDetect Service;
    }


    protected ServiceDetectorItem itemStart = null;
    protected ServiceDetectorItem cursor = null;

    public ServiceDetectors() {
        itemStart = null;
        cursor = null;
    }



    public void Add(ServiceDetect Service) {
        ServiceDetectorItem item = new ServiceDetectorItem();
        item.Service = Service;

        if (itemStart == null) {
            itemStart = item;
            return;
        }
        if (cursor == null)
            cursor = itemStart;

        if (cursor.Next != null)
            cursor.Next.Prev = item;
        item.Prev = cursor;
        item.Next = cursor.Next;
        cursor.Next = item;

        cursor = item;
    }


    /// <summary>
    /// Try to locate the specified item inside the list and return the item
    /// </summary>
    /// <param name="Service"></param>
    /// <returns>The located object item or NULL if the object was not found</returns>
    protected ServiceDetectorItem GetItem(ServiceDetect Service) {
        ServiceDetectorItem item = itemStart;
        while (item != null) {
            if (item.Service == Service) {
                cursor = item;
                return item;
            }
            item = item.Next;
        }
        return null;
    }


    /// <summary>
    /// Try to locate the specified item inside the list and set the cursor to the coreesponding item. It the item was not found, the cursor will not be moved
    /// </summary>
    /// <param name="Service"></param>
    /// <returns>True if found, False otherwise</returns>
    public bool LocateItem(ServiceDetect Service) {
        ServiceDetectorItem item = GetItem(Service);
        return (item == null) ? false : true;
    }



    /// <summary>
    /// Remove the specified item
    /// </summary>
    /// <param name="Service"></param>
    public void Remove(ServiceDetect Service) {
        ServiceDetectorItem item = GetItem(Service);
        if (item == null)
            return;

        ServiceDetectorItem othItem = (item.Prev != null) ? item.Prev : item.Next;

        if (item.Next != null)
            item.Next.Prev = item.Prev;
        if (item.Prev != null)
            item.Prev.Next = item.Next;

        if (item == cursor)
            cursor = othItem;

        if (item == itemStart)
            itemStart = othItem;
    }



    /// <summary>
    /// Remove the current list item
    /// </summary>
    public void Remove() {
        ServiceDetectorItem item = cursor;

        if (cursor == null)
            return;

        item = (cursor.Prev != null) ? cursor.Prev : cursor.Next;

        if (cursor.Prev != null)
            cursor.Prev.Next = cursor.Next;
        if (cursor.Next != null)
            cursor.Next.Prev = cursor.Prev;

        if (cursor == itemStart)
            itemStart = item;
        cursor = item;
    }



    /// <summary>
    /// Get is empty or not
    /// </summary>
    public bool IsEmpty { get { return (itemStart == null); } }

    IEnumerator IEnumerable.GetEnumerator() {
        return (IEnumerator)this;
    }


    public ServiceDetectors GetEnumerator() {
        return this;
    }



    object IEnumerator.Current {
        get { return (cursor == null) ? null : cursor.Service; }
    }



    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
    public bool MoveNext() {
        if (itemStart == null)
            return false;
        if (cursor == null) {
            cursor = itemStart;
            return true;
        }
        if (cursor.Next == null)
            return false;
        cursor = cursor.Next;
        return true;
    }




    /// <summary>
    /// Sets the enumerator to its initial position, which is before the first element in the collection.
    /// </summary>
    public void Reset() {
        cursor = itemStart;
    }


    public void Dispose() { }
}

