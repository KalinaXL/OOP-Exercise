using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Adapters;
using OOP_Exercise.System_Classes;
using OOP_Exercise.Utility_Classes;

namespace OOP_Exercise.Fragments
{
    class DialogSubject : DialogFragment
    {
        ListView lsSubject;
        ProgressBar progress;
        TextView textView;
        bool isMidTerm;
        string subjectName;
        ImageView img;
        public DialogSubject(bool isMidTerm, string subjectName)
        {
            this.isMidTerm = isMidTerm;
            this.subjectName = subjectName;
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);
            View view = inflater.Inflate(Resource.Layout.dialog_term_subject, container, false);
            lsSubject = view.FindViewById<ListView>(Resource.Id.lsview_term_subject);
            progress = view.FindViewById<ProgressBar>(Resource.Id.bar_sub_term);
            textView = view.FindViewById<TextView>(Resource.Id.txtSubTerm);
            img = view.FindViewById<ImageView>(Resource.Id.imgCancel);
            lsSubject.ItemClick += LsSubject_ItemClickAsync;         
            
            return view;
        }

        async Task<bool> GetQuestions(string year)
        {
            bool isSuccess = (await FireBaseHelper.GetSubExam(DataManager.IsMidTerm ? "Mid" : "Final", subjectName, year).ConfigureAwait(true));
            if (isSuccess)
            {
                int lenOfQuesList = FireBaseHelper.Ques.Count;
                if (lenOfQuesList == 0)
                {
                    await FireBaseHelper.LoadDataFromFile(DataManager.IsMidTerm ? "Mid" : "Final", subjectName, year).ConfigureAwait(true);
                    if (FireBaseHelper.Ques == null)
                        return false;
                    lenOfQuesList = FireBaseHelper.Ques.Count;
                    if (lenOfQuesList == 0)
                        return false;
                }
                
            }
            else
            {
                await FireBaseHelper.LoadDataFromFile(DataManager.IsMidTerm ? "Mid" : "Final", subjectName, year).ConfigureAwait(true);
                if (FireBaseHelper.Ques == null)
                    return false;
                int lenOfQuesList = FireBaseHelper.Ques.Count;
                if (lenOfQuesList == 0)
                    return false;
                //DataManager.QuestionsList = FireBaseHelper.Ques;
                //TOTAL_TIME = FireBaseHelper.Time * 60;
                //for (int i = 0; i < lenOfQuesList; i++)
                //{
                //    DataManager.CurrQuesList.Add(new CurrentQuestion(i, false));
                //}

            }
            return true;
        }
        private async void LsSubject_ItemClickAsync(object sender, AdapterView.ItemClickEventArgs e)
        {
            textView.Visibility = progress.Visibility = ViewStates.Visible;
            lsSubject.Visibility = ViewStates.Gone;
            string year = (string) e.Parent.GetItemAtPosition(e.Position);
            bool isFinished = await GetQuestions(year).ConfigureAwait(true);
            if (isFinished)
            {
                Handler h = new Handler();
                Action action = () =>
                {
                    Intent intent = new Intent(this.Activity, typeof(QuizActivity));
                    Bundle bundle = new Bundle();
                    bundle.PutString("SubjectName", subjectName);
                    intent.PutExtras(bundle);

                    this.Activity.StartActivity(intent);
                    Dismiss();
                };
                h.Post(action);
            }
            else
            {
                Dismiss();
                Toast.MakeText(this.Activity, "Không có dữ liệu về môn học này !!!", ToastLength.Short).Show();
            }

        }

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            bool isSuccess = await FireBaseHelper.GetTermYear(isMidTerm ? "Mid" : "Final", subjectName);
            SubTermAdapter adapter;
            if (isSuccess)
            {
                textView.Visibility = progress.Visibility = ViewStates.Gone;
                adapter = new SubTermAdapter(this.Activity, FireBaseHelper.Terms);
                lsSubject.SetAdapter(adapter);
                lsSubject.Visibility = ViewStates.Visible;
            }
            else
            {
                if (File.Exists(DatabaseUtility.jsonPath))
                {
                    List<string> listOfTerm = await FireBaseHelper.LoadTermFromFile(isMidTerm ? "Mid" : "Final", subjectName);
                    if (listOfTerm != null)
                    {
                        textView.Visibility = progress.Visibility = ViewStates.Gone;
                        adapter = new SubTermAdapter(this.Activity, listOfTerm);
                        lsSubject.SetAdapter(adapter);
                        lsSubject.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        progress.Visibility = ViewStates.Gone;
                        img.Visibility = textView.Visibility = ViewStates.Visible;
                        textView.Text = "Không có dữ liệu !!!";
                    }
                }
                else
                {
                    progress.Visibility = ViewStates.Gone;
                    img.Visibility = textView.Visibility = ViewStates.Visible;
                    textView.Text = "Không có dữ liệu !!!";

                }
            }
            
        }

      
    }
}