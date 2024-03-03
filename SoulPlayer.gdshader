shader_type canvas_item;

uniform bool immortal = true;

void fragment() {
	vec4 col = texture(TEXTURE, UV);
	if(immortal)
		col.a*=pow(sin(TIME*5.0),2.0)*0.7+0.3; 
	
	COLOR = col;
}