using System.Drawing;

namespace AERMOD.LIB.Componentes.StyleProgressBar
{
    public class Cores
    {
        public Color ColorInterior(Natural Natural)
        {
            Color empty = Color.Empty;
            Cores.Natural natural = Natural;
            if (natural <= Cores.Natural.MoradoOscuro)
            {
                if (natural <= Cores.Natural.AzulClaro)
                {
                    if (natural <= Cores.Natural.Cyan)
                    {
                        switch (natural)
                        {
                            case Cores.Natural.Negro:
                                return 0x1a1a1a.ToColor();

                            case Cores.Natural.Teal:
                                return 0x4d40.ToColor();

                            case Cores.Natural.Cyan:
                                return 0x6064.ToColor();
                        }
                        return empty;
                    }
                    switch (natural)
                    {
                        case Cores.Natural.A700Teal:
                            return 0x613f.ToColor();

                        case Cores.Natural.A700verde:
                            return 0x9921.ToColor();

                        case Cores.Natural.AzulClaro:
                            return 0x1579b.ToColor();
                    }
                    return empty;
                }
                if (natural <= Cores.Natural.Indigo)
                {
                    switch (natural)
                    {
                        case Cores.Natural.Azul:
                            return 0xd47a1.ToColor();

                        case Cores.Natural.A700azul:
                            return 0xd47a1.ToColor();

                        case Cores.Natural.Indigo:
                            return 0x1a237e.ToColor();
                    }
                    return empty;
                }
                if (natural <= Cores.Natural.GrisAzul)
                {
                    switch (natural)
                    {
                        case Cores.Natural.Verde:
                            return 0x1b5e20.ToColor();

                        case Cores.Natural.GrisAzul:
                            return 0x37474f.ToColor();
                    }
                    return empty;
                }
                switch (natural)
                {
                    case Cores.Natural.A700verdeClaro:
                        return 0x117d00.ToColor();

                    case Cores.Natural.MoradoOscuro:
                        return 0x311b92.ToColor();
                }
                return empty;
            }
            if (natural <= Cores.Natural.Defaul)
            {
                if (natural <= Cores.Natural.Morado)
                {
                    switch (natural)
                    {
                        case Cores.Natural.Marron:
                            return 0x4e342e.ToColor();

                        case Cores.Natural.VerdeClaro:
                            return 0x33691e.ToColor();

                        case Cores.Natural.Morado:
                            return 0x4a148c.ToColor();
                    }
                    return empty;
                }
                if (natural <= Cores.Natural.A700lima)
                {
                    switch (natural)
                    {
                        case Cores.Natural.Gris:
                            return 0x424242.ToColor();

                        case Cores.Natural.A700lima:
                            return 0x82ba00.ToColor();
                    }
                    return empty;
                }
                switch (natural)
                {
                    case Cores.Natural.Lima:
                        return 0x827717.ToColor();

                    case Cores.Natural.Defaul:
                        return 0xbdbdbd.ToColor();
                }
                return empty;
            }
            if (natural <= Cores.Natural.NaranjaRojo)
            {
                switch (natural)
                {
                    case Cores.Natural.Rosa:
                        return 0x880e4f.ToColor();

                    case Cores.Natural.Rojo:
                        return 0xb71c1c.ToColor();

                    case Cores.Natural.NaranjaRojo:
                        return 0xdd2c00.ToColor();
                }
                return empty;
            }
            if (natural <= Cores.Natural.Ambar)
            {
                switch (natural)
                {
                    case Cores.Natural.Naranja:
                        return 0xe65100.ToColor();

                    case Cores.Natural.Ambar:
                        return 0xff6f00.ToColor();
                }
                return empty;
            }
            switch (natural)
            {
                case Cores.Natural.Amarillo:
                    return 0xf57f17.ToColor();

                case Cores.Natural.Blanco:
                    return 0xbdbdba.ToColor();
            }
            return empty;
        }

        public Color ColorOrganico(Natural Natural)
        {
            Color empty = Color.Empty;
            Cores.Natural natural = Natural;
            if (natural <= Cores.Natural.MoradoOscuro)
            {
                if (natural <= Cores.Natural.AzulClaro)
                {
                    if (natural <= Cores.Natural.Cyan)
                    {
                        switch (natural)
                        {
                            case Cores.Natural.Negro:
                                return 0x2b2b2b.ToColor();

                            case Cores.Natural.Teal:
                                return 0x796b.ToColor();

                            case Cores.Natural.Cyan:
                                return 0x97a7.ToColor();
                        }
                        return empty;
                    }
                    switch (natural)
                    {
                        case Cores.Natural.A700Teal:
                            return 0x8f70.ToColor();

                        case Cores.Natural.A700verde:
                            return 0x9921.ToColor();

                        case Cores.Natural.AzulClaro:
                            return 0x288d1.ToColor();
                    }
                    return empty;
                }
                if (natural <= Cores.Natural.Indigo)
                {
                    switch (natural)
                    {
                        case Cores.Natural.Azul:
                            return 0x1976d2.ToColor();

                        case Cores.Natural.A700azul:
                            return 0x5ec2.ToColor();

                        case Cores.Natural.Indigo:
                            return 0x303f9f.ToColor();
                    }
                    return empty;
                }
                if (natural <= Cores.Natural.GrisAzul)
                {
                    switch (natural)
                    {
                        case Cores.Natural.Verde:
                            return 0x388e3c.ToColor();

                        case Cores.Natural.GrisAzul:
                            return 0x455a64.ToColor();
                    }
                    return empty;
                }
                switch (natural)
                {
                    case Cores.Natural.A700verdeClaro:
                        return 0x3aad00.ToColor();

                    case Cores.Natural.MoradoOscuro:
                        return 0x512da8.ToColor();
                }
                return empty;
            }
            if (natural <= Cores.Natural.Defaul)
            {
                if (natural <= Cores.Natural.Morado)
                {
                    switch (natural)
                    {
                        case Cores.Natural.Marron:
                            return 0x5d4037.ToColor();

                        case Cores.Natural.VerdeClaro:
                            return 0x689f38.ToColor();

                        case Cores.Natural.Morado:
                            return 0x7b1fa2.ToColor();
                    }
                    return empty;
                }
                if (natural <= Cores.Natural.A700lima)
                {
                    switch (natural)
                    {
                        case Cores.Natural.Gris:
                            return 0x616161.ToColor();

                        case Cores.Natural.A700lima:
                            return 0x9fd900.ToColor();
                    }
                    return empty;
                }
                switch (natural)
                {
                    case Cores.Natural.Lima:
                        return 0xafb42b.ToColor();

                    case Cores.Natural.Defaul:
                        return 0xcfcfcf.ToColor();
                }
                return empty;
            }
            if (natural <= Cores.Natural.NaranjaRojo)
            {
                switch (natural)
                {
                    case Cores.Natural.Rosa:
                        return 0xc2185b.ToColor();

                    case Cores.Natural.Rojo:
                        return 0xd32f2f.ToColor();

                    case Cores.Natural.NaranjaRojo:
                        return 0xe64a19.ToColor();
                }
                return empty;
            }
            if (natural <= Cores.Natural.Ambar)
            {
                switch (natural)
                {
                    case Cores.Natural.Naranja:
                        return 0xf57c00.ToColor();

                    case Cores.Natural.Ambar:
                        return 0xffa000.ToColor();
                }
                return empty;
            }
            switch (natural)
            {
                case Cores.Natural.Amarillo:
                    return 0xfbc02d.ToColor();

                case Cores.Natural.Blanco:
                    return 0xe0e0e0.ToColor();
            }
            return empty;
        }

        public Natural EnumNatural(Organico organico)
        {
            Natural azul = Natural.Azul;
            Organico organico2 = organico;
            if (organico2 <= Organico.MoradoOscuro)
            {
                if (organico2 <= Organico.AzulClaro)
                {
                    if (organico2 <= Organico.A700Teal)
                    {
                        switch (organico2)
                        {
                            case Organico.A700azul:
                                return Natural.A700azul;

                            case Organico.Teal:
                                return Natural.Teal;

                            case Organico.A700Teal:
                                return Natural.A700Teal;
                        }
                        return azul;
                    }
                    switch (organico2)
                    {
                        case Organico.Cyan:
                            return Natural.Cyan;

                        case Organico.A700verde:
                            return Natural.A700verde;

                        case Organico.AzulClaro:
                            return Natural.AzulClaro;
                    }
                    return azul;
                }
                if (organico2 <= Organico.Indigo)
                {
                    switch (organico2)
                    {
                        case Organico.Azul:
                            return Natural.Azul;

                        case Organico.Negro:
                            return Natural.Negro;

                        case Organico.Indigo:
                            return Natural.Indigo;
                    }
                    return azul;
                }
                if (organico2 <= Organico.A700verdeClaro)
                {
                    switch (organico2)
                    {
                        case Organico.Verde:
                            return Natural.Verde;

                        case Organico.A700verdeClaro:
                            return Natural.A700verdeClaro;
                    }
                    return azul;
                }
                switch (organico2)
                {
                    case Organico.GrisAzul:
                        return Natural.GrisAzul;

                    case Organico.MoradoOscuro:
                        return Natural.MoradoOscuro;
                }
                return azul;
            }
            if (organico2 <= Organico.Rosa)
            {
                if (organico2 <= Organico.VerdeClaro)
                {
                    switch (organico2)
                    {
                        case Organico.Marron:
                            return Natural.Marron;

                        case Organico.Gris:
                            return Natural.Gris;

                        case Organico.VerdeClaro:
                            return Natural.VerdeClaro;
                    }
                    return azul;
                }
                if (organico2 <= Organico.A700lima)
                {
                    switch (organico2)
                    {
                        case Organico.Morado:
                            return Natural.Morado;

                        case Organico.A700lima:
                            return Natural.A700lima;
                    }
                    return azul;
                }
                switch (organico2)
                {
                    case Organico.Lima:
                        return Natural.Lima;

                    case Organico.Rosa:
                        return Natural.Rosa;
                }
                return azul;
            }
            if (organico2 <= Organico.Blanco)
            {
                switch (organico2)
                {
                    case Organico.Defaul:
                        return Natural.Defaul;

                    case Organico.Rojo:
                        return Natural.Rojo;

                    case Organico.Blanco:
                        return Natural.Blanco;
                }
                return azul;
            }
            if (organico2 <= Organico.Naranja)
            {
                switch (organico2)
                {
                    case Organico.NaranjaRojo:
                        return Natural.NaranjaRojo;

                    case Organico.Naranja:
                        return Natural.Naranja;
                }
                return azul;
            }
            switch (organico2)
            {
                case Organico.Amarillo:
                    return Natural.Amarillo;

                case Organico.Ambar:
                    return Natural.Ambar;
            }
            return azul;
        }

        public Organico EnumOrganico(Natural natural)
        {
            Organico azul = Organico.Azul;
            Natural natural2 = natural;
            if (natural2 <= Natural.MoradoOscuro)
            {
                if (natural2 <= Natural.AzulClaro)
                {
                    if (natural2 <= Natural.Cyan)
                    {
                        switch (natural2)
                        {
                            case Natural.Negro:
                                return Organico.Negro;

                            case Natural.Teal:
                                return Organico.Teal;

                            case Natural.Cyan:
                                return Organico.Cyan;
                        }
                        return azul;
                    }
                    switch (natural2)
                    {
                        case Natural.A700Teal:
                            return Organico.A700Teal;

                        case Natural.A700verde:
                            return Organico.A700verde;

                        case Natural.AzulClaro:
                            return Organico.AzulClaro;
                    }
                    return azul;
                }
                if (natural2 <= Natural.Indigo)
                {
                    switch (natural2)
                    {
                        case Natural.Azul:
                            return Organico.Azul;

                        case Natural.A700azul:
                            return Organico.A700azul;

                        case Natural.Indigo:
                            return Organico.Indigo;
                    }
                    return azul;
                }
                if (natural2 <= Natural.GrisAzul)
                {
                    switch (natural2)
                    {
                        case Natural.Verde:
                            return Organico.Verde;

                        case Natural.GrisAzul:
                            return Organico.GrisAzul;
                    }
                    return azul;
                }
                switch (natural2)
                {
                    case Natural.A700verdeClaro:
                        return Organico.A700verdeClaro;

                    case Natural.MoradoOscuro:
                        return Organico.MoradoOscuro;
                }
                return azul;
            }
            if (natural2 <= Natural.Defaul)
            {
                if (natural2 <= Natural.Morado)
                {
                    switch (natural2)
                    {
                        case Natural.Marron:
                            return Organico.Marron;

                        case Natural.VerdeClaro:
                            return Organico.VerdeClaro;

                        case Natural.Morado:
                            return Organico.Morado;
                    }
                    return azul;
                }
                if (natural2 <= Natural.A700lima)
                {
                    switch (natural2)
                    {
                        case Natural.Gris:
                            return Organico.Gris;

                        case Natural.A700lima:
                            return Organico.A700lima;
                    }
                    return azul;
                }
                switch (natural2)
                {
                    case Natural.Lima:
                        return Organico.Lima;

                    case Natural.Defaul:
                        return Organico.Defaul;
                }
                return azul;
            }
            if (natural2 <= Natural.NaranjaRojo)
            {
                switch (natural2)
                {
                    case Natural.Rosa:
                        return Organico.Rosa;

                    case Natural.Rojo:
                        return Organico.Rojo;

                    case Natural.NaranjaRojo:
                        return Organico.NaranjaRojo;
                }
                return azul;
            }
            if (natural2 <= Natural.Ambar)
            {
                switch (natural2)
                {
                    case Natural.Naranja:
                        return Organico.Naranja;

                    case Natural.Ambar:
                        return Organico.Ambar;
                }
                return azul;
            }
            switch (natural2)
            {
                case Natural.Amarillo:
                    return Organico.Amarillo;

                case Natural.Blanco:
                    return Organico.Blanco;
            }
            return azul;
        }

        public enum Interior
        {
            A700azul = 0xd47a1,
            A700lima = 0x82ba00,
            A700Teal = 0x613f,
            A700verde = 0x9921,
            A700verdeClaro = 0x117d00,
            Amarillo = 0xf57f17,
            Ambar = 0xff6f00,
            Azul = 0xd47a1,
            AzulClaro = 0x1579b,
            Blanco = 0xbdbdba,
            Cyan = 0x6064,
            Defaul = 0xbdbdbd,
            Gris = 0x424242,
            GrisAzul = 0x37474f,
            Indigo = 0x1a237e,
            Lima = 0x827717,
            Marron = 0x4e342e,
            Morado = 0x4a148c,
            MoradoOscuro = 0x311b92,
            Naranja = 0xe65100,
            NaranjaRojo = 0xdd2c00,
            Negro = 0x1a1a1a,
            Rojo = 0xb71c1c,
            Rosa = 0x880e4f,
            Teal = 0x4d40,
            Verde = 0x1b5e20,
            VerdeClaro = 0x33691e
        }

        public enum Natural
        {
            A700azul = 0x2962ff,
            A700lima = 0xaeea00,
            A700Teal = 0xbfa5,
            A700verde = 0xc853,
            A700verdeClaro = 0x64dd17,
            Amarillo = 0xffeb3b,
            Ambar = 0xffc107,
            Azul = 0x2196f3,
            AzulClaro = 0x3a9f4,
            Blanco = 0xffffff,
            Cyan = 0xbcd4,
            Defaul = 0xdedede,
            Gris = 0x9e9e9e,
            GrisAzul = 0x607d8b,
            Indigo = 0x3f51b5,
            Lima = 0xcddc39,
            Marron = 0x795548,
            Morado = 0x9c27b0,
            MoradoOscuro = 0x673ab7,
            Naranja = 0xff9800,
            NaranjaRojo = 0xff5722,
            Negro = 0,
            Rojo = 0xf44336,
            Rosa = 0xe91e63,
            Teal = 0x9688,
            Verde = 0x4caf50,
            VerdeClaro = 0x8bc34a
        }

        public enum Organico
        {
            A700azul = 0x5ec2,
            A700lima = 0x9fd900,
            A700Teal = 0x8f70,
            A700verde = 0x9921,
            A700verdeClaro = 0x3aad00,
            Amarillo = 0xfbc02d,
            Ambar = 0xffa000,
            Azul = 0x1976d2,
            AzulClaro = 0x288d1,
            Blanco = 0xe0e0e0,
            Cyan = 0x97a7,
            Defaul = 0xcfcfcf,
            Gris = 0x616161,
            GrisAzul = 0x455a64,
            Indigo = 0x303f9f,
            Lima = 0xafb42b,
            Marron = 0x5d4037,
            Morado = 0x7b1fa2,
            MoradoOscuro = 0x512da8,
            Naranja = 0xf57c00,
            NaranjaRojo = 0xe64a19,
            Negro = 0x2b2b2b,
            Rojo = 0xd32f2f,
            Rosa = 0xc2185b,
            Teal = 0x796b,
            Verde = 0x388e3c,
            VerdeClaro = 0x689f38
        }
    }
}
