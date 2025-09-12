using System.Collections.Generic;

public class CircleDeleter
{
    public List<Circle> Delete(List<Circle> list, int i)
    {
        list.RemoveAt(i);
        return list;
    }
}
