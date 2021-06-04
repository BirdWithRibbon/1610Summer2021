// In this case, result = 4
float result = Mathf.Lerp(3f, 5f, 0.5f)

    Vector3 from = new Vector3(1f, 2f, 3f);
Vector3 to = new Vector3(5f, 6f, 7f);

// Here result = (4, 5, 6)
Vector3 result = Vector3.Lerp(from, to, 0.75f);

void Update()
{
    light.intensity = Mathf.Lerp(light.intensity, 8f, 0.5f);
}

void Update()
{
    light.intensity = Mathf.Lerp(light.intensity, 8f, 0.5f * Time.deltaTime);
}