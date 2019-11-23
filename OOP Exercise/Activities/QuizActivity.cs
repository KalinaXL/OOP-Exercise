using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Activities;
using OOP_Exercise.Adapters;
using OOP_Exercise.Fragments;
using OOP_Exercise.System_Classes;
using OOP_Exercise.Utility_Classes;
using System;
using System.Timers;

namespace OOP_Exercise
{
    [Activity(Label = "QuizActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class QuizActivity : AppCompatActivity
    {
        static int TOTAL_TIME = 0;
        RecyclerView answerSheetView;
        Timer countDownTimer;
        AnswerSheetAdapter adapter;


        TextView txtTimer;
        TextView txtQuesAns;
        Button btnSubmit;

        TabLayout tabLayout;
        ViewPager viewPager;
        QuizFragmentAdapter quizFragmentAdapter;

        int numOfQues = 0;
        static int REQUESTCODE = 9999;

        protected override void OnDestroy()
        {
            if (countDownTimer != null)
                countDownTimer.Stop();
            base.OnDestroy();
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_quiz);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            
            TextView txtSubjecTest = FindViewById<TextView>(Resource.Id.txtSubjectTest);
            txtSubjecTest.Text = Intent.GetStringExtra("SubjectName");

            InitAgain();
           
           
                    
            txtQuesAns = FindViewById<TextView>(Resource.Id.txt_question_answered);
            txtTimer = FindViewById<TextView>(Resource.Id.txt_timer);
            txtQuesAns.Visibility = txtTimer.Visibility = ViewStates.Visible;
            txtQuesAns.Text = String.Format("0/{0}", numOfQues);

            btnSubmit = FindViewById<Button>(Resource.Id.btnSubmit);
            answerSheetView = FindViewById<RecyclerView>(Resource.Id.grid_answer);
            answerSheetView.HasFixedSize = true;
            answerSheetView.SetLayoutManager(new GridLayoutManager(this, 10));
            adapter = new AnswerSheetAdapter(this, DataManager.CurrQuesList);
            answerSheetView.SetAdapter(adapter);
            TimerCountDown();
            viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            tabLayout = FindViewById<TabLayout>(Resource.Id.sliding_tabs);

            GenerateFragmentList();

            quizFragmentAdapter = new QuizFragmentAdapter(SupportFragmentManager, this, DataManager.FragmentQuizList);
            viewPager.Adapter = quizFragmentAdapter;

            tabLayout.SetupWithViewPager(viewPager);
            btnSubmit.Click += BtnSubmit_Click;
            
           
        }


        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (!DataManager.IsReadResult)
            {
                Android.Support.V7.App.AlertDialog.Builder dialog = new Android.Support.V7.App.AlertDialog.Builder(this);
                dialog.SetCancelable(true);
                dialog.SetPositiveButton("YES", delegate
                {
                    countDownTimer.Stop();
                    Intent intent = new Intent(this, typeof(ResultActivity));
                    intent.PutExtra("Answers", DataManager.AnswersChoosed);
                    StartActivityForResult(intent, REQUESTCODE);

                }
                );
                dialog.SetNegativeButton("NO", delegate { });
                Android.Support.V7.App.AlertDialog alert = dialog.Create();
                alert.SetTitle("Thông báo");
                alert.SetMessage("Bạn có chắc chắn muốn nộp bài?");
                alert.Show();
            }
            else
            {
                Intent intent = new Intent(this, typeof(ResultActivity));
                intent.PutExtra("Answers", DataManager.AnswersChoosed);
                StartActivityForResult(intent, REQUESTCODE);
                DataManager.IsReadResult = false;

            }

        }

        private void GenerateFragmentList()
        {
            if (DataManager.FragmentQuizList.Count > 0)
                DataManager.FragmentQuizList.Clear();
            int numberOfQues = DataManager.QuestionsList.Count;
            for (int i = 0; i < numberOfQues; i++)
            {
                Bundle bundle = new Bundle();
                bundle.PutInt("index", i);
                FragmentQuiz fragQuiz = new FragmentQuiz();
                fragQuiz.Arguments = bundle;
                fragQuiz.RadioChanged += FragQuiz_RadioChanged;
                DataManager.FragmentQuizList.Add(fragQuiz);
            }
            DataManager.AnswersChoosed = new byte[numberOfQues];
        }

        private void FragQuiz_RadioChanged(object sender, EventArgs e)
        {
            if (DataManager.IsReadResult)
                return;
            adapter.NotifyDataSetChanged();
            RunOnUiThread(() =>
            {
                txtQuesAns.Text = String.Format("{0}/{1}", DataManager.NumOfQuesAnswered, numOfQues);
            });
        }

        void TimerCountDown()
        {
            if (countDownTimer == null)
            {
                countDownTimer = new System.Timers.Timer(1000);
                countDownTimer.Enabled = true;
                countDownTimer.Elapsed += CountDownTimer_Elapsed;
                countDownTimer.Start();
            }
            else
            {
                //Set TOTAL_TIME again
                countDownTimer.Stop();
                countDownTimer.Start();
            }
        }

        private void CountDownTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            --TOTAL_TIME;
            if (TOTAL_TIME == 0)
            {
                countDownTimer.Stop();
                Intent intent = new Intent(this, typeof(ResultActivity));
                intent.PutExtra("Answers", DataManager.AnswersChoosed);
                StartActivityForResult(intent, REQUESTCODE);
            }
            RunOnUiThread(() =>
            {
                txtTimer.Text = $"{TOTAL_TIME / 60}:{TOTAL_TIME % 60}";
            }
            );

        }


        public async void InitAgain()
        {
            if (DataManager.QuestionsList.Count > 0)
            {
                DataManager.QuestionsList.Clear();
                DataManager.CurrQuesList.Clear();
                DataManager.FragmentQuizList.Clear();
                for (int i = 0; i < DataManager.AnswersChoosed.Length; i++)
                    DataManager.AnswersChoosed[i] = 0;
            }
            DataManager.NumOfQuesAnswered = 0;
            DataManager.QuestionsList = FireBaseHelper.Ques;
            TOTAL_TIME = FireBaseHelper.Time * 60;
            numOfQues = DataManager.QuestionsList.Count;
            for (int i = 0; i < numOfQues; i++)
            {
                DataManager.CurrQuesList.Add(new CurrentQuestion(i, false));
            }
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if (drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
        
            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

   
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == REQUESTCODE)
            {
                if (resultCode == Android.App.Result.Ok)
                {
                    string action = data.GetStringExtra("Action");
                    if (!String.IsNullOrEmpty(action))
                    {
                        if (action == "Come_back")
                        {
                            Finish();
                        }
                        else if (action == "View_Answer")
                        {
                            countDownTimer.Stop();
                            for (int i = 0; i < DataManager.FragmentQuizList.Count; i++)
                                DataManager.FragmentQuizList[i].Validate();
                        }
                    }
                }
            }

        }

    }
}