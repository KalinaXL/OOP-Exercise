using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Support.V7.App;
using Android.Widget;
using OOP_Exercise.Utility_Classes;
using OOP_Exercise.Adapters;
using OOP_Exercise.Fragments;

namespace OOP_Exercise.Activities
{
    [Activity(Label = "ResultActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class ResultActivity : AppCompatActivity
    {
        TextView txtLevelGain, txtNumRightQues;
        byte[] answersOfUser;
        StateOfQuestion[] stateOfQues;
        RecyclerView recyclerView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_result);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Kết quả";
            SetSupportActionBar(toolbar);

            txtLevelGain = FindViewById<TextView>(Resource.Id.txtLevelGain);
            txtNumRightQues = FindViewById<TextView>(Resource.Id.txtNumRightQues);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerViewAns);
          
           

            answersOfUser = Intent.GetByteArrayExtra("Answers");

            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            
            EvaluateAnswer();
            recyclerView.HasFixedSize = true;
            recyclerView.SetLayoutManager(new GridLayoutManager(this, 2));
            var adapter = new QuestionValAdapter(this, stateOfQues);
            recyclerView.SetAdapter(adapter);
            recyclerView.AddItemDecoration(new SpaceDecoration(20));


            // Create your application here
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.result_item, menu);
            return base.OnCreateOptionsMenu(menu);
        }
    
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    DataManager.IsReadResult = false;
                    Intent intent = new Intent();
                    intent.PutExtra("Action", "Come_back");
                    SetResult(Android.App.Result.Ok, intent);
                    Finish();
                    return true;
                case Resource.Id.menu_view_answer:
                    ViewQuizAnswer();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }

        }

        private void ViewQuizAnswer()
        {
            DataManager.IsReadResult = true;
            Intent intent = new Intent();
            intent.PutExtra("Action", "View_Answer");
            SetResult(Android.App.Result.Ok, intent);
            Finish();
        }

        void EvaluateAnswer()
        {
            int numRight = 0;
            int numOfQues = answersOfUser.Length;

            stateOfQues = new StateOfQuestion[numOfQues];
            for (int i = 0; i < numOfQues; i++)
            {
                if (answersOfUser[i] == 0)
                {
                    stateOfQues[i] = StateOfQuestion.MISSED;
                   
                }
                else if (answersOfUser[i] == DataManager.QuestionsList[i].CorrectAnswer)
                {
                    stateOfQues[i] = StateOfQuestion.RIGHT;
                    ++numRight;
                }
                else
                {
                    stateOfQues[i] = StateOfQuestion.WRONG;
                }
            }
            RunOnUiThread(() => 
            {
                txtNumRightQues.Text = $"{numRight}/{numOfQues}";
                double fraction = (double)numRight / numOfQues;
                if (fraction >= .9)
                    txtLevelGain.Text = "Xuất sắc! Bạn cần giữ cho kiến thức luôn tươi mới";
                else if (fraction >= .8)
                    txtLevelGain.Text = "Giỏi! Bạn cần giừ cho kiến thức luôn tươi mới";
                else if (fraction >= .7)
                    txtLevelGain.Text = "Khá! Bạn cần luyện tập thêm";
                else if (fraction >= .5)
                    txtLevelGain.Text = "Bạn cần ôn tập kĩ hơn";
                else
                    txtLevelGain.Text = "Bạn cần học hành nghiêm túc hơn!";
            });
           
        }
           

    }
}