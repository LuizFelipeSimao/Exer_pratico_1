﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace exercicio_pratico_1
{
    public partial class Form1 : Form
    {
        //Criando um vetor dinamico da Classe Filme
        Dictionary<int, List<Filme>> dicionario_filmes = new Dictionary<int, List<Filme>>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Ligando os Grupos do ListVIew aos valores do Combobox
            lista_genero.DataSource = listView1.Groups;
        }




        //-------------Tab control para cadastro de filmes----------------------------//




        private void cadastrar_Click(object sender, EventArgs e)
        {
            Filme filme = new Filme();
            //armazenando os valor digitados pelo usuario na classe
            filme.Nome = nome_filme.Text;
            filme.Genero = lista_genero.SelectedItem.ToString();
            filme.Data_Assistido = data.Text;
            filme.Local = local.Text;
            if (dicionario_filmes.ContainsKey(lista_genero.SelectedIndex))
            {
                List<Filme> l = dicionario_filmes[lista_genero.SelectedIndex];
                l.Add(filme);
            }
            else
            {
                //Criando uma lista de classe de filme
                List<Filme> lista_filme = new List<Filme>();
                //armazenando a classe na lista
                lista_filme.Add(filme);
                //armazenando a lista de filmes de um mesmo genero no dicionario
                dicionario_filmes.Add(lista_genero.SelectedIndex, lista_filme);
            }
            //criando um item para listview
            ListViewItem novo_item = new ListViewItem();
            novo_item.Text = filme.Nome;
            novo_item.Group = listView1.Groups[lista_genero.SelectedIndex];
            novo_item.SubItems.Add(filme.Data_Assistido);
            novo_item.SubItems.Add(filme.Local);
            //adicionando o item no listview de apresentação
            listView1.Items.Add(novo_item);
        }

        //evento para remover filmes da lista
        private void remover_Click(object sender, EventArgs e)
        {
            //atribui a variavel auxiliar o valor do indice do grupo selecionado no listview
            int auxiliar = listView1.Groups.IndexOf(listView1.SelectedItems[0].Group);
            //atribui a variavel comparacao o valor do item selecionado no listview
            string comparacao = listView1.SelectedItems[0].Text;
            List<Filme> lista = dicionario_filmes[auxiliar];
            for(int i = 0; i< lista.Count; ++i)
            {
                Filme l = lista[i];
                //verifica se o nome do objeto na lista é igual ao nome do item do listview
                if (l.Nome == comparacao)
                {
                    //teste
                    // MessageBox.Show("" + l.Nome + " " + " " + l.Genero + " " + l.Data_Assistido + " " + l.Local, "", MessageBoxButtons.OK);
                    
                    //remove o item da lista e do listview
                    lista.Remove(l);
                    listView1.SelectedItems[0].Remove();
                }
            }

        }

        //evento para editar um filme na lista
        private void editar_Click(object sender, EventArgs e)
        {
            //atribui a variavel auxiliar o valor do indice do grupo selecionado no listview
            int auxiliar = listView1.Groups.IndexOf(listView1.SelectedItems[0].Group);
            //atribui a variavel comparacao o valor do item selecionado no listview
            string comparacao = listView1.SelectedItems[0].Text;
            List<Filme> lista = dicionario_filmes[auxiliar];
            for (int i = 0; i < lista.Count; ++i)
            {
                Filme l = lista[i];
                //verifica se o nome do objeto na lista é igual ao nome do item do listview
                if (l.Nome == comparacao)
                {
                    //grava no objeto os valores dos textbox
                    l.Nome = nome_filme.Text;
                    l.Genero = lista_genero.Text;
                    l.Data_Assistido = data.Text;
                    l.Local = local.Text;
                    if (dicionario_filmes.ContainsKey(lista_genero.SelectedIndex))
                    {
                        List<Filme> listaexistente = dicionario_filmes[lista_genero.SelectedIndex];
                        listaexistente.Add(l);
                    }
                    else
                    {
                        //Criando uma lista de classe de filme
                        List<Filme> lista_filme = new List<Filme>();
                        //armazenando a classe na lista
                        lista_filme.Add(l);
                        //armazenando a lista de filmes de um mesmo genero no dicionario
                        dicionario_filmes.Add(lista_genero.SelectedIndex, lista_filme);
                    }
                    lista.Remove(l);
                    
                    //atualiza o list view com os novos valores
                    ListViewItem lv_editando = listView1.SelectedItems[0];
                    lv_editando.Group = listView1.Groups[lista_genero.SelectedIndex];
                    lv_editando.SubItems[0].Text = l.Nome;
                    lv_editando.SubItems[1].Text = l.Data_Assistido;
                    lv_editando.SubItems[2].Text = l.Local;
                }
            }
            cadastrar.Enabled = true;
            remover.Enabled = true;
            editar.Enabled = false;
            listView1.Enabled = true;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            cadastrar.Enabled = false;
            remover.Enabled = false;
            editar.Enabled = true;
            listView1.Enabled = false;
        }



        //------------------------tabcontrol para pesquisa-------------------------//

    }
}