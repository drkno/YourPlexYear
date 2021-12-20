using System;
using System.Collections.Generic;

namespace YourPlexYear.Service.Tautulli.Service;

public static class ListExtensions
{
    public static T GetOnly<T>(this List<T> list, string error)
    {
        if (list.Count != 1)
        {
            throw new ArgumentException(error);
        }
        return list.GetFirst(error);
    }
    
    public static T GetFirst<T>(this List<T> list, string error)
    {
        if (list.Count == 0)
        {
            throw new ArgumentException(error);
        }
        return list.GetFirstOrNull();
    }
    
    public static T GetFirstOrNull<T>(this List<T> list)
    {
        return list.Count == 0 ? default : list[0];
    }
}
