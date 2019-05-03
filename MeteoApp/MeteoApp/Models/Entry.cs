using System;

namespace MeteoApp
{
    public class Entry
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Text { get; internal set; }
        public bool IsReadOnly { get; internal set; }
    }
}
