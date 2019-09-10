using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
using OOP_Exercise.Adapters;

namespace OOP_Exercise
{
    [Activity(Label = "QuizActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class QuizActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        int TOTAL_TIME = 45 * 60;
        RecyclerView answerSheetView;
        Timer countDownTimer;
        AnswerSheetAdapter adapter;
        List<CurrentQuestion> currQuesList;
        List<Question> questionList;
       
        TextView txtTimer;
        TextView txtQuesAns;
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
            SetSupportActionBar(toolbar);

           
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);

            GetQuestions();
            if (questionList.Count > 0)
            {
                txtQuesAns = FindViewById<TextView>(Resource.Id.txt_question_answered);
                txtTimer = FindViewById<TextView>(Resource.Id.txt_timer);
                txtQuesAns.Visibility = txtTimer.Visibility = ViewStates.Visible;
                txtQuesAns.Text = "0/20";
                TimerCountDown();

                answerSheetView = FindViewById<RecyclerView>(Resource.Id.grid_answer);
                answerSheetView.HasFixedSize = true;
                answerSheetView.SetLayoutManager(new GridLayoutManager(this, 10));
                adapter = new AnswerSheetAdapter(this, currQuesList);
                answerSheetView.SetAdapter(adapter);
            }
        }
        void TimerCountDown()
        {
            if (countDownTimer == null)
            {
                countDownTimer = new Timer(1000);
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
                countDownTimer.Stop();
            RunOnUiThread(() =>
            {
                txtTimer.Text = $"{TOTAL_TIME / 60}:{TOTAL_TIME % 60}";
            }
            );

        }

        private void GetQuestions()
        {
            currQuesList = new List<CurrentQuestion>();
            questionList = new List<Question>()
            {
                new Question("A","a","a","a","a",1),
                 new Question("A","a","a","a","a",1),
                  new Question("A","a","a","a","a",1),
                  new Question("A","a","a","a","a",1),
                  new Question("A","a","a","a","a",1),
            };

            int lenOfQuesList = questionList.Count;
            for (int i = 0; i < lenOfQuesList; i++)
            {
                currQuesList.Add(new CurrentQuestion(i, false));
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
           // MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            //int id = item.ItemId;
            //if (id == Resource.Id.action_settings)
            //{
            //    return true;
            //}

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            //if (id == Resource.Id.nav_camera)
            //{
            //    // Handle the camera action
            //}
            //else if (id == Resource.Id.nav_gallery)
            //{

            //}
            //else if (id == Resource.Id.nav_slideshow)
            //{

            //}
            //else if (id == Resource.Id.nav_manage)
            //{

            //}
            //else if (id == Resource.Id.nav_share)
            //{

            //}
            //else if (id == Resource.Id.nav_send)
            //{

            //}

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
    }
}