using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class Trie
    {
        public class Treenode
        {
            public Treenode firstchild;
            public Treenode sibling;
            public bool leaf;
            public char c;

            public Treenode()
            {
                firstchild = null;
                sibling = null;
                leaf = false;
            }

            public Treenode(char letter)
            {
                firstchild = null;
                sibling = null;
                leaf = false;
                c = letter;
            }
        }

        private Treenode root = new Treenode();

        public Trie(){ }

        public Trie(string pathname)
        {
            string[] stringarray = System.IO.File.ReadAllLines(pathname);

            foreach (string word in stringarray)
            {
                this.Add(word);
            }

        }

        public void Add(string word)
        {
            if (word == "")
                throw new System.ArgumentException("Parameter cannot be empty string");

            Treenode node = root;

            foreach (char letter in word)
            {
                if (node.firstchild == null)
                {
                    node.firstchild = new Treenode(letter);
                    node = node.firstchild;
                }

                else
                {
                    if (node.firstchild.c != letter)
                    {
                        if (node.firstchild.sibling != null)
                        {
                            node = node.firstchild.sibling;
                            while (node.c != letter & node.sibling != null)
                            {
                                node = node.sibling;
                            }

                            if (node.c == letter)
                            {
                            }

                            else if (node.sibling == null)
                            {
                                node.sibling = new Treenode(letter);
                                node = node.sibling;
                            }

                        }


                        else if (node.firstchild.sibling == null)
                        {
                            node.firstchild.sibling = new Treenode(letter);
                            node = node.firstchild.sibling;
                        }
                    }

                    else if (node.firstchild.c == letter)
                    {
                        node = node.firstchild;
                    }

                }
            }

            if (node.leaf == false)
            {
                node.leaf = true;
            }
        }

        public bool Contains(string word)
        {
            if (word == "")
                return false;

            Treenode node = root;

            foreach (char letter in word)
            {
                if (node.firstchild == null)
                {
                    return false;
                }

                else
                {
                    if (node.firstchild.c != letter)
                    {
                        if (node.firstchild.sibling != null)
                        {
                            node = node.firstchild.sibling;
                            while (node.c != letter & node.sibling != null)
                            {
                                node = node.sibling;
                            }

                            if (node.c == letter) { }

                            else if (node.sibling == null)
                            {
                                return false;
                            }
                        }


                        else if (node.firstchild.sibling == null)
                        {
                            return false;
                        }
                    }

                    else if (node.firstchild.c == letter)
                    {
                        node = node.firstchild;
                    }
                } 
            }

            if (node.leaf == true)
            {
                return true;
            }
            return false;
        }

    }
}
