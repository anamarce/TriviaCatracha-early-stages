using UnityEngine;
using System.Collections;

public class EffectsManager : MonoBehaviour {
	

	

    public GameObject PlayConstantEffect(GameObject particleResource, Vector3 pos)
    {
       

        if (particleResource != null)
        {


            var goP = (GameObject)Instantiate(particleResource, pos, Quaternion.identity);

            var ps = goP.GetComponent<ParticleSystem>();

            ps.Play();

            return goP;

        }
        return null;
    }

  
	public GameObject PlayConstantEffect(string particleResource,Vector3 pos)
	{
		
		
		if(particleResource!=string.Empty)
		{
		    
			
		 var goP = (GameObject)Instantiate(Resources.Load(particleResource),pos,Quaternion.identity);
		  	
	      var  ps= goP.GetComponent<ParticleSystem>();
	  
	      ps.Play();
		  
		  return goP;
		
		}
		return null;
	}

    public void  PlayEffect(GameObject particleResource, Vector3 pos)
    {
      

        if (particleResource != null)
        {


            var goP = (GameObject)Instantiate(particleResource, pos, Quaternion.identity);

            var ps = goP.GetComponent<ParticleSystem>();

            ps.Play();

         
            Destroy(goP, ps.duration);

        }
       
    }
	//This is for a particle resource with no looping
	public void PlayEffect(string particleResource,Vector3 pos,Quaternion rotation, float duration,bool loadbestfit=true,
		                   bool useDefault=true)
	{
		

		if(particleResource!=string.Empty)
		{
		  string temp;
		  GameObject goP;
		  GameObject particlePrefab=null;
		  if (loadbestfit)
			{
				temp = particleResource + Managers.Platform.GetPlatformResolutionPostFix();
				
		   	    particlePrefab = Resources.Load(temp) as GameObject;
		  
			} 
		  //---------
			
		  if (particlePrefab==null&&useDefault==false)
					return;
		  if (particlePrefab==null)
			{
			  	 particlePrefab = Resources.Load(particleResource) as GameObject;
				
			}
			
			if (rotation==Quaternion.identity)
		  	     goP = (GameObject)Instantiate(particlePrefab,pos,particlePrefab.transform.rotation);
			else
				 goP = (GameObject)Instantiate(particlePrefab,pos,rotation);
			
			var ps= goP.GetComponent<ParticleSystem>();
		    ps.Play();
		   Destroy(goP,duration);
			
		}
	}
}
