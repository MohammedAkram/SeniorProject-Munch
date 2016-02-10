package md52d092a3298bfdf816f326cc672685199;


public class Activity2
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("test.Droid.Activity2, test.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Activity2.class, __md_methods);
	}


	public Activity2 () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Activity2.class)
			mono.android.TypeManager.Activate ("test.Droid.Activity2, test.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
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
