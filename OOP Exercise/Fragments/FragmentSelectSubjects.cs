using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;

using Android.Util;
using Android.Views;

namespace OOP_Exercise.Fragments
{
    public class FragmentSelectSubjects : Fragment
    {
        Android.Support.V7.Widget.Toolbar toolbar;
        RecyclerView recyclerView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            View view = inflater.Inflate(Resource.Layout.fragment_select_subject, container, false);
            toolbar = view.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Chọn môn";

            recyclerView = view.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new GridLayoutManager(this.Activity, 2));

            SubjectAdapter adapter = new SubjectAdapter(this.Activity, new List<string> { "Giải tích 1", "Hóa đại cương", "Vật lí 1", "Cấu trúc rời rạc","Hệ thống số","Nhập môn điện toán" });
            recyclerView.AddItemDecoration(new SpaceDecoration(20));
            recyclerView.SetAdapter(adapter);
            return view;
        }
    }
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