package md5ea9257fae210c0c5127d800d09245f41;


public class QuestionValAdapter_MyViewVal
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("OOP_Exercise.Adapters.QuestionValAdapter+MyViewVal, OOP Exercise", QuestionValAdapter_MyViewVal.class, __md_methods);
	}


	public QuestionValAdapter_MyViewVal (android.view.View p0)
	{
		super (p0);
		if (getClass () == QuestionValAdapter_MyViewVal.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.Adapters.QuestionValAdapter+MyViewVal, OOP Exercise", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
