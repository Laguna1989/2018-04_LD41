uniform sampler2D tex;
uniform float strength;

float rand(vec2 co)
{
    return fract(sin(dot(co.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

void main()
{
    if(strength == 0.0f)
    {
        vec2 coord = vec2(gl_TexCoord[0].x, gl_TexCoord[0].y);
        gl_FragColor = texture(tex, coord);
    }
    else
    {
        float dx = strength * (1.0 / 800.0);
        float dy = strength * (1.0 / 600.0);

        vec2 coord = vec2
		(
                dx * floor(gl_TexCoord[0].x / dx),
                dy * floor(gl_TexCoord[0].y / dy)
        );

        vec4 pixcol = texture(tex, coord);

        if(pixcol.r != 255 || pixcol.g != 255 || pixcol.b != 255)
        {
            pixcol.r = strength * rand(pixcol.xy);
            pixcol.g = strength * rand(pixcol.xy);
            pixcol.b = strength * rand(pixcol.xy);
        }

        gl_FragColor = pixcol;
        //gl_FragColor = texture(tex, coord);
    }
}