package md56ef1c2f256bd0fc180a8e8b98081d7c4;


public class SlashScreen
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("OOP_Exercise.Activities.SlashScreen, OOP_Exercise", SlashScreen.class, __md_methods);
	}


	public SlashScreen ()
	{
		super ();
		if (getClass () == SlashScreen.class)
			mono.android.TypeManager.Activate ("OOP_Exercise.Activities.SlashScreen, OOP_Exercise", "", this, new java.lang.Object[] {  });
	}


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
