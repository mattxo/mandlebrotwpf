#version 330

uniform vec2 center;
uniform float scale;
uniform int maxIterations;
uniform int frameIndex;
uniform bool cycleColors;
uniform float colorCompressionFactor;

in vec4 fragmentPosition;
in vec4 mousePosition;

out vec4 color;

#include "Shaders/FractalFunctions.glsl";
#include "Shaders/ComplexNumberFunctions.glsl";

vec2 initializeC()
{
	float x = fragmentPosition.x * scale + center.x;
	float y = fragmentPosition.y * scale - center.y;

	return vec2(x, y);
}


void main()
{			
	vec2 c = initializeC();
	vec2 z = c;    
			
	int iteration;

	while (iteration < maxIterations && (z.x * z.x + z.y * z.y) < 100)
	{				
		#equation

		iteration++;
	}

	color = calculateColor(iteration, maxIterations, z, frameIndex, cycleColors, colorCompressionFactor);	
}
