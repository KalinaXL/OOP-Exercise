package md59292efd5466a1dae0285c0a6bc841169;


public class ThreadSharedPrefes
	extends java.lang.Thread
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_run:()V:GetRunHandler\n" +
			"";
		mono.android.Runtime.register ("OOP_Exercise.ThreadSharedPrefes, OOP_Exercise", ThreadSharedPrefes.class, __md_methods);
	}


	public ThreadSharedPrefes ()
	{
		super ();
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP_Exercise", "", this, new java.lang.Object[] {  });
	}

	public ThreadSharedPrefes (boolean p0, android.content.Context p1, java.lang.String p2, java.lang.String p3)
	{
		super ();
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP_Exercise", "System.Boolean, mscorlib:Android.Content.Context, Mono.Android:System.String, mscorlib:System.String, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void run ()
	{
		n_run ();
	}

	private native void n_run ();

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
