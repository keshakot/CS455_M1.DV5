using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFire //: Seek
{
    public Kinematic character;
    public GameObject target;
    public GameObject projectilePrefab;

    float muzzleV = 26.0f;
    Vector3 offset = new Vector3(0, 2, 0);
    Vector3 gravity;
    

    public ProjectileFire( GameObject dtpf ){
        projectilePrefab = dtpf;
        //dummyTarget = UnityEngine.Object.Instantiate(dtpf, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    	character = character;
    	Physics.gravity = new Vector3(0, -9.8f, 0);
    	gravity = Physics.gravity;
    }

    public bool fireProjectile()
    {   
    	float ttt = 0.0f;

    	// Calculate delta from character to target
    	Vector3 delta = target.transform.position - character.transform.position - offset;

    	// Calculate equation coefficients
    	float a = Mathf.Pow(gravity.magnitude, 2);
    	float b = -4 * ( Vector3.Dot(gravity, delta) + muzzleV * muzzleV );
    	float c = 4 * Mathf.Pow(delta.magnitude, 2);

    	// Check for no real solutions
    	Debug.Log("-A-");
    	float b2m4ac = b*b-4*a*c;
    	if( b2m4ac < 0 )
    		return false;

    	// Find the candidate times 
    	float time0 = Mathf.Sqrt( (-b + Mathf.Sqrt(b2m4ac)) / (2*a) );
    	float time1 = Mathf.Sqrt( (-b - Mathf.Sqrt(b2m4ac)) / (2*a) );

    	// Select time to target
    	if( time0 < 0 ){
    		if( time1 < 0 )
    			return false;
    		else
    			ttt = time1;
    	}
    	else{
    		if( time1 < 0 )
    			ttt = time0;
    		else
    			ttt = Mathf.Min(time0, time1);
    	}

    	/* Calculate the firing vector */
    	/*Debug.Log("a: " + a);
    	Debug.Log("b: " + b);
    	Debug.Log("c: " + c);
    	Debug.Log("b2m4ac: " + b2m4ac);
    	Debug.Log("-----");
    	Debug.Log("delta: " + delta);
    	Debug.Log("gravity: " + gravity);
    	Debug.Log("time0: " + time0);
    	Debug.Log("time1: " + time1);
    	Debug.Log("ttt: " + ttt);*/

    	Vector3 fireVector = (delta * 2 - gravity * (ttt*ttt)) / (2 * /*muzzleV * */ttt);
    	Debug.Log("fireVector: " + fireVector);
    	//Vector3 fireVector = (delta * 2 - gravity * (ttt*ttt)) / (2 * muzzleV * ttt);

    	/* Fire the projectile */
		GameObject projectile = UnityEngine.Object.Instantiate(projectilePrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject; 
		projectile.transform.position = character.transform.position + offset;
		projectile.GetComponent<Rigidbody>().velocity = fireVector;

    	return true;
    }
}
