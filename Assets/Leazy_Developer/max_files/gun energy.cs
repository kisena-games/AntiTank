 using UnityEngine;

public class ParticleControl : MonoBehaviour
{
    public ParticleSystem[] particles; // Массив партиклов
    private bool areParticlesActive = true;
    public float reactivationDelay = 5f; // Время задержки перед повторным включением

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (areParticlesActive)
            {
                StopAllCoroutines();
                DisableParticles();
            }
        }
    }

    private void DisableParticles()
    {
        foreach (var particle in particles)
        {
            particle.Stop();
        }
        areParticlesActive = false;
        Invoke("ReactivateParticles", reactivationDelay); // Запускаем метод для повторного включения
    }

    private void ReactivateParticles()
    {
        StartCoroutine(ActivateParticles());
    }

    System.Collections.IEnumerator ActivateParticles()
    {
        for (int i = 0; i < particles.Length; i += 2)
        {
            if (i < particles.Length)
            {
                particles[i].Play();
            }
            if (i + 1 < particles.Length)
            {
                particles[i + 1].Play();
            }
            yield return new WaitForSeconds(0.2f);
        }
        areParticlesActive = true;
    }
}
