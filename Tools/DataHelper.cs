using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Tools
{
    public static class DataHelper
    {
        public static T GetRandomElement<T>(params T[] objects)
        {
            var random = new Random();
            var randomIndex = random.Next(objects.GetLowerBound(0), objects.GetUpperBound(0) + 1);
            return objects[randomIndex];
        }
    }
}
