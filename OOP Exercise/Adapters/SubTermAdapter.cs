using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;


namespace OOP_Exercise.Adapters
{
    class SubTermAdapter : BaseAdapter<string>
    {
        List<string> subterm;
        Context context;
        public SubTermAdapter(Context context, List<string> subterm)
        {
            this.context = context;
            this.subterm = subterm;
        }
        public override string this[int position]
        {
            get => subterm[position];
        }

        public override int Count
        {
            get => subterm.Count;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            if (convertView == null)
                convertView = LayoutInflater.From(context).Inflate(Resource.Layout.item_term_subject, null, false);
            convertView.FindViewById<TextView>(Resource.Id.txtTermSub).Text = subterm[position];

            return convertView;
        }
    }
}