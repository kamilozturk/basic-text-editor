using System.Windows;

namespace MyTextEditor
{
    public class MyTab : DependencyObject
    {
        private string _content;
        private bool _modified;

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(MyTab), new PropertyMetadata(null));


        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                Modified = _content != value;

                _content = value;
            }
        }
        public int TabNo { get; set; }
        public string Path { get; set; }


        public bool Modified
        {
            get { return _modified; }
            set { _modified = value; ModifiedIcon = value ? "o" : "X"; }
        }

        public string ModifiedIcon
        {
            get { return (string)GetValue(ModifiedIconProperty); }
            set { SetValue(ModifiedIconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModifiedIcon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModifiedIconProperty =
            DependencyProperty.Register("ModifiedIcon", typeof(string), typeof(MyTab), new PropertyMetadata("X"));
    }
}
