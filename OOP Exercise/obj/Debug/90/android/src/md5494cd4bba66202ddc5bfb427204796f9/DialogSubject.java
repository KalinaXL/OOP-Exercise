package md5494cd4bba66202ddc5bfb427204796f9;


public class DialogSubject
	extends android.support.v4.app.DialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreateView:(Landroid/view/LayoutInflater;Landroid/view/ViewGroup;Landroid/os/Bundle;)Landroid/view/View;:GetOnCreateView_Landroid_view_LayoutInflater_Landroid_view_ViewGroup_Landroid_os_Bundle_Handler\n" +
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("OOP_Exercise.Fragments.DialogSubject, OOP_Exercise", DialogSubject.class, __md_methods);
	}


	public DialogSubject ()
	{
		super ();
		if (getClass () == DialogSubject.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.Fragments.DialogSubject, OOP_Exercise", "", this, new java.lang.Object[] {  });
	}

	public DialogSubject (boolean p0, java.lang.String p1)
	{
		super ();
		if (getClass () == DialogSubject.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.Fragments.DialogSubject, OOP_Exercise", "System.Boolean, mscorlib:System.String, mscorlib", this, new java.lang.Object[] { p0, p1 });
	}


	public android.view.View onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2)
	{
		return n_onCreateView (p0, p1, p2);
	}

	private native android.view.View n_onCreateView (android.view.LayoutInflater p0, android.view.ViewGroup p1, android.os.Bundle p2);


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
