using Android.Graphics;
using Android.Support.V7.Widget;
using Android.Views;

namespace OOP_Exercise.Fragments
{
    public class SpaceDecoration : RecyclerView.ItemDecoration
    {
        int space;
        public SpaceDecoration(int space)
        {
            this.space = space;
        }
        public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
        {
            outRect.Left = outRect.Right = outRect.Top = outRect.Bottom = space;
        }
    }
}