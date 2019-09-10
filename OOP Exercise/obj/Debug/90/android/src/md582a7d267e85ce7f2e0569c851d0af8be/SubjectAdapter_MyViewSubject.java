package md582a7d267e85ce7f2e0569c851d0af8be;


public class SubjectAdapter_MyViewSubject
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("OOP_Exercise.Fragments.SubjectAdapter+MyViewSubject, OOP Exercise", SubjectAdapter_MyViewSubject.class, __md_methods);
	}


	public SubjectAdapter_MyViewSubject (android.view.View p0)
	{
		super (p0);
		if (getClass () == SubjectAdapter_MyViewSubject.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.Fragments.SubjectAdapter+MyViewSubject, OOP Exercise", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
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
