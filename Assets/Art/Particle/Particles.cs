using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem baseParticle;
    public List<ParticleSystem> particles;

    public void ActivateBaseParticle()
    {
        baseParticle.Play();
    }

    public void ActivateAllParticle()
    {
        foreach(ParticleSystem p in particles)
        {
            p.Play();
        }
    }

    public void DisableParticle()
    {
        baseParticle.Stop();
        foreach (ParticleSystem p in particles)
        {
            p.Stop();   
        }
    }
}
