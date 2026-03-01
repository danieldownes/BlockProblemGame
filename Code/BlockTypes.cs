using static Colour;

public class BlockTypes
{
	public record struct TypeDef(int Count, Colour Top, Colour Bottom, Colour Left, Colour Right, bool HasDiamond);

	public static readonly TypeDef[] Data =
	{
		//                     T       B       L       R      Diamond
		new(3,                Green,  Yellow, Blue,   Red,   true),
		new(3,                Blue,   Green,  Yellow, Red,   true),
		new(1,                Blue,   Yellow, Green,  Red,   true),
		new(1,                Yellow, Yellow, Green,  Red,   true),
		new(1,                Green,  Yellow, Blue,   Red,   false),
		new(2,                Green,  Blue,   Yellow, Red,   false),
		new(5,                Blue,   Green,  Yellow, Red,   false)
	};
}
