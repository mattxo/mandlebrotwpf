const vec4 interiorColor = vec4(0, 0, 0, 0);

// All components are in the range [0…1], including hue.
vec3 hsv2rgb(vec3 c)
{
	vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);

	return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

float normalizeIterations(int iterations, vec2 z)
{
	return iterations - log2(log2(dot(z, z))) + 4.0;
}

vec3 normalizedHsl(int iterations, int maxIterations, vec2 z, int frameIndex, bool cycleColors, float colorCompressionFactor)
{
	float normalisedIterations = normalizeIterations(iterations, z);

	float h = normalisedIterations / float(maxIterations) / colorCompressionFactor;
	float s = 1;
	float l = 1;

	if (cycleColors)
	{
		h += mod(float(frameIndex) / 1000.0, 1);
	}
	    
	vec3 hsl = vec3(h, s, l);
	vec3 rgb = hsv2rgb(hsl);

	return rgb;
}

vec4 normalizedColor(int iterations, int maxIterations, vec2 z, int frameIndex, bool cycleColors, float colorCompressionFactor)
{
	vec3 hsl = normalizedHsl(iterations, maxIterations, z, frameIndex, cycleColors, colorCompressionFactor);
	vec4 rgba = vec4(hsl, 0);

	return rgba;
}

vec4 calculateColor(int iterations, int maxIterations, vec2 z, int frameIndex, bool cycleColors, float colorCompressionFactor)
{
	if (iterations == maxIterations)
	{
		return interiorColor;
	}

	return normalizedColor(iterations, maxIterations, z, frameIndex, cycleColors, colorCompressionFactor);
}