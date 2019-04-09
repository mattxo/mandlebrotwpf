#version 330

layout(location = 0) in vec4 vertexPosition;
uniform mat4 projection;
uniform vec2 u_mousePosition;

out vec4 fragmentPosition; 
out vec4 mousePosition;

void main(void)
{
    gl_Position = projection * vertexPosition;
	fragmentPosition = vertexPosition;
	mousePosition = projection * vec4(u_mousePosition.x, u_mousePosition.y, 0.5, 0.5);
}