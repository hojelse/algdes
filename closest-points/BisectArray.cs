using System.Collections;

class BisectArray<T> : IEnumerable<T>
{
  int leftIdx;
  int rightIdx;
  private readonly BisectArrayRoot _bisectArrayRoot;

  public BisectArray(T[] array)
  {
    this._bisectArrayRoot = new BisectArrayRoot(array);
    this.leftIdx = 0;
    this.rightIdx = this._bisectArrayRoot.Length;
  }

  private BisectArray(BisectArray<T> bisectArray, int leftIdx, int rightIdx)
  {
    this._bisectArrayRoot = bisectArray._bisectArrayRoot;
    this.leftIdx = leftIdx;
    this.rightIdx = rightIdx;
  }

  public IEnumerator<T> GetEnumerator()
  {
    for (int i = leftIdx; i < rightIdx; i++)
    {
      yield return _bisectArrayRoot[i];
    }
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    return this.GetEnumerator();
  }

  /// <summary>
  /// This method bisects this into two halfs.
  /// <example>
  /// For example:
  /// <code>
  /// var array = new BisectArray(new int[] { 1,2,3,4,5,6,7,8 });
  ///
  /// (var left, var right) = top.Bisect();
  /// </code>
  /// </example>
  /// </summary>
  /// <remarks>
  /// If this has an odd number of elements then right will have one more element than left.
  /// </remarks>
  /// <returns>
  /// A tuple of two BisectArray, for the left and right half of this.
  /// </returns>
  public (BisectArray<T> left, BisectArray<T> right) Bisect()
  {
    int count = this.rightIdx - this.leftIdx;
    int mid = this.leftIdx + (count/2);
    return (
      new BisectArray<T>(this, this.leftIdx, mid),
      new BisectArray<T>(this, mid, this.rightIdx)
    );
  }

  public T this[int index]
  {
    get => _bisectArrayRoot[index];
    set => _bisectArrayRoot[index] = value;
  }

  private class BisectArrayRoot
  {
    public readonly T[] array;
    public int Length;

    public T this[int index] {
      get => array[index];
      set => array[index] = value;
    }

    public BisectArrayRoot(T[] array)
    {
      this.array = array;
      this.Length = array.Length;
    }
  }

}
