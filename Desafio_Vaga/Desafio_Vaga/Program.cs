using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Desafio_Vaga
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] numeros;
            Console.Write("Digite respectivamente os tamanhos X e Y na mesma linha: ");//
            numeros = Regex.Split(Console.ReadLine(), @"\D+");
            int tamX = Int32.Parse(numeros[0]);
            int tamY = Int32.Parse(numeros[1]);

            // nova horta
            Horta horta = new Horta(tamX, tamY);

            Console.Write("Posicao inicial\t\t: ");  // ex de entradas: 4 4 (5,1) ( 0 , 0 )
            numeros = Regex.Split(Console.ReadLine(), @"\D+");
            int initX = Int32.Parse(numeros[0]);
            int initY = Int32.Parse(numeros[1]);

            Console.Write("Orientacao inicial\t: "); // N L S O
            char initDir =  Console.ReadLine().Trim().ToCharArray()[0];

            Robo robo = new Robo(initX, initY, initDir);

            horta.setRobo(robo);

            do
            {
                Console.WriteLine("Mínimo de canteiros: 1");
                Console.Write("Canteiros a irrigar: ");// ex de entradas: 4 4 (5,1) ( 0 , 0 )
                numeros = Regex.Split(Console.ReadLine(), @"\D+");
            } while (numeros.Length < 2);

            for (int i = 0; i < numeros.Length -1; i+=2) // somente adicionará canteiros com X e Y, caso tenha qtd impar, ignora o ultimo n°
            {
                horta.addCanteiro(Int32.Parse(numeros[i]), Int32.Parse(numeros[i + 1]));
            }

            foreach (Canteiro c in horta.getCanteiros())
            {
                robo.achaCaminho(c.getX(), c.getY());
            }

            Console.WriteLine(robo.getCaminho());
            Console.WriteLine("Orientacao final: {0}", robo.getOrientacao());
        }

    }

    class Horta
    {
        private int x, y;
        private ArrayList canteiros = new ArrayList();
        private Robo robo;

        public Horta(int x, int y)
        {
            if(x < 0 || y < 0)
            {
                Console.WriteLine("Tamanho mínimo do canteiro é 1 x 1");
                Environment.Exit(2);
            }
            this.x = x;
            this.y = y;
        }

        public void setRobo(Robo robo)
        {
            if (robo.getX() > this.x - 1 || robo.getY() > this.y - 1 || robo.getX() < 0 || robo.getY() < 0) //
            {
                Console.WriteLine("Robo em posição invalida");
                Environment.Exit(2);
            }
            this.robo = robo;
        }

        public void addCanteiro(int x, int y)
        {
            if(x > this.x - 1 || y > this.y - 1 || x < 0 || y < 0) //-1 pois o indice do canteiro começa em 0
            {
                Console.WriteLine("Posicao de canteiro invalida");
                return;
            }

            canteiros.Add(new Canteiro(x, y));
        }

        public ArrayList getCanteiros()
        {
            return canteiros;
        }
    }

    class Robo
    {
        private char[] direcoes = { 'N', 'L', 'S', 'O' };
        private int direcao;
        private int x, y;
        private String caminho = "Caminho\t\t:";

        public Robo(int x, int y, char direcao)
        {
            direcao = char.ToUpper(direcao); // garante que vai receber um caracter maiusculo
            this.x = x;
            this.y = y;
            this.direcao = Array.IndexOf(direcoes, direcao);
            if (this.direcao == -1)
            {
                Console.WriteLine("Direcao invalida");
                Environment.Exit(2);
            }
        }

        public char getOrientacao()
        {
            return direcoes[direcao];
        }

        public String getCaminho()
        {
            return caminho;
        }

        //retorna x
        public int getX()
        {
            return this.x;
        }

        //retorna y
        public int getY()
        {
            return this.y;
        }

        public void achaCaminho(int x, int y)
        {
            int i, direcaoRef = Array.IndexOf(direcoes, 'L'); // N L S O N... 
            while(this.x != x || this.y != y)
            {
                if(this.x == x) //após o robo chegar no X correto, será encontrado o caminho até Y
                {
                    if(this.y < y)
                    {
                        virar(Array.IndexOf(direcoes, 'N'));
                        for (i = this.y; i < y; i++)
                            movimento();
                    }
                    else
                    {
                        virar(Array.IndexOf(direcoes, 'S'));
                        for (i = y; i < this.y; i++)
                            movimento();
                    }
                }
                else if(this.x < x)// quando o robo estiver a Oeste da posicao do canteiro
                {
                    virar(Array.IndexOf(direcoes, 'L'));
                    for (i = this.x; i < x; i++)
                        movimento();
                }
                else // quando o robo estiver a Leste da posicao do canteiro
                {
                    virar(Array.IndexOf(direcoes, 'O'));
                    for (i = x; i < this.x; i++)
                        movimento();
                }
            }
            irrigar();
        }

        //faz as direcoes que o robo deve virar para chegar de uma direcao para outra, 
        //o parametro é o indice da direcao desejada
        public void virar(int direcao)
        {
            int aux =(this.direcao - direcao + 4) % 4;
            switch (aux)
            {
                case 0:
                    return;
                case 1:
                    virarEsq();
                    break;
                case 2:
                    virarDir();
                    virarDir();
                    break;
                case 3:
                    virarDir();
                    break;
            }
        }

        //Movimenta o robo para frente na direção que ele está virado
        public void movimento()
        {
            switch (direcoes[direcao])
            {
                case 'N':
                    y++;
                    break;
                case 'S':
                    y--;
                    break;
                case 'L':
                    x++;
                    break;
                case 'O':
                    x--;
                    break;
            }
            caminho += " M";
        }

        //Vira o robô para direita
        public void virarDir()
        {
            direcao = (++direcao) % 4;
            caminho += " D";
        }

        //Vira o robô para esquerda
        public void virarEsq()
        {
           direcao = (direcao + 3) % 4;
            caminho += " E";
        }

        public void irrigar()
        {
            caminho += " I";
        }
    }

    class Canteiro
    {
        private int x, y;

        public Canteiro(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }
    }


}
