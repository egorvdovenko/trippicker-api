﻿namespace demo_web_api.Models
{
    public class ListItem<T>
    {
        public T Id { get; set; }
        public string Text { get; set; }
    }
}
