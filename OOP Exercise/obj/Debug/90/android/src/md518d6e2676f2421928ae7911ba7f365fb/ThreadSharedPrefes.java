package md518d6e2676f2421928ae7911ba7f365fb;


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
		mono.android.Runtime.register ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", ThreadSharedPrefes.class, __md_methods);
	}


	public ThreadSharedPrefes ()
	{
		super ();
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "", this, new java.lang.Object[] {  });
	}


	public ThreadSharedPrefes (java.lang.Runnable p0)
	{
		super (p0);
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "Java.Lang.IRunnable, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public ThreadSharedPrefes (java.lang.Runnable p0, java.lang.String p1)
	{
		super (p0, p1);
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "Java.Lang.IRunnable, Mono.Android:System.String, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public ThreadSharedPrefes (java.lang.String p0)
	{
		super (p0);
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "System.String, mscorlib", this, new java.lang.Object[] { p0 });
	}


	public ThreadSharedPrefes (java.lang.ThreadGroup p0, java.lang.Runnable p1)
	{
		super (p0, p1);
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "Java.Lang.ThreadGroup, Mono.Android:Java.Lang.IRunnable, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public ThreadSharedPrefes (java.lang.ThreadGroup p0, java.lang.Runnable p1, java.lang.String p2)
	{
		super (p0, p1, p2);
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "Java.Lang.ThreadGroup, Mono.Android:Java.Lang.IRunnable, Mono.Android:System.String, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public ThreadSharedPrefes (java.lang.ThreadGroup p0, java.lang.Runnable p1, java.lang.String p2, long p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "Java.Lang.ThreadGroup, Mono.Android:Java.Lang.IRunnable, Mono.Android:System.String, mscorlib:System.Int64, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public ThreadSharedPrefes (java.lang.ThreadGroup p0, java.lang.String p1)
	{
		super (p0, p1);
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "Java.Lang.ThreadGroup, Mono.Android:System.String, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}

	public ThreadSharedPrefes (android.content.Context p0, java.lang.String p1, java.lang.String p2)
	{
		super ();
		if (getClass () == ThreadSharedPrefes.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.ThreadSharedPrefes, OOP Exercise", "Android.Content.Context, Mono.Android:System.String, mscorlib:System.String, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
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
