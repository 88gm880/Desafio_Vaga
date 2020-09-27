using System;
using System.Collections;

namespace Desafio_Vaga
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int initX, initY;
            Horta horta = new Horta(10, 10);
            horta.addCanteiro(4, 4);
            horta.addCanteiro(0, 0);
            horta.addCanteiro(0, 5);
            //Console.Write("Posicao inicial\t: (");
            //Console.ReadLine();

            Robo robo = new Robo(4, 4, 'L');

            foreach (Canteiro c in horta.getCanteiros())
            {
                robo.achaCaminho(c.getX(), c.getY());
            }

            Console.WriteLine(robo.getCaminho());
            Console.WriteLine("Orientacao final: " + robo.getOrientacao());
        }

    }

    class Horta
    {
        private int x, y;
        private ArrayList canteiros = new ArrayList();

        public Horta(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void addCanteiro(int x, int y)
        {
            if(x > this.x || y > this.y)
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
        private String caminho = "Caminho: ";

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
