using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spriteget : MonoBehaviour
{
    public Image  i_get,i_set,cor;
    public int r,rr;
    public Image pol,spash,berry;
    public Sprite dark,white,berry_s;
    public Sprite[] pol_s,spash_s,random_bery1,random_bery2,random_bery3,random_bery4;
    public GameObject [] s;
    
        // Start is called before the first frame update
    void Start()
    {

        
        r=Random.Range(0,3);
        

        if(i_get.sprite.name.Contains("Raspberry-5"))
        {
          pol.sprite=pol_s[0];
          rr=r;
          spash.sprite=spash_s[rr];
          
          i_set.sprite=random_bery1[r];
        }
        if(i_get.sprite.name.Contains("Blueberry-3"))
        {
          pol.sprite=pol_s[1];
          rr=r+4;
          spash.sprite=spash_s[rr];
          
          i_set.sprite=random_bery2[r];
        }
        
        if(i_get.sprite.name.Contains("strawberry-1"))
        {
          pol.sprite=pol_s[2];
          rr=r+8;
          spash.sprite=spash_s[rr];
          
          i_set.sprite=random_bery3[r];
        }
        if(i_get.sprite.name.Contains("Gooseberry-4-1"))
        {
          pol.sprite=pol_s[3];
          rr=r+12;
          spash.sprite=spash_s[rr];
          i_set.sprite=random_bery4[r];
        }
    }
    public void Mode1()
    {
s[0].SetActive(false);
s[1].SetActive(false);
s[2].SetActive(true);
cor.sprite=dark;


    }
public void Mode2()
    {
s[0].SetActive(false);
s[1].SetActive(false);
s[3].SetActive(true);
cor.sprite=dark;


    }
    public void Mode3()
    {
s[0].SetActive(false);
s[1].SetActive(false);
s[4].SetActive(true);
cor.sprite=dark;


    }
    public void Regen()
    {
         
        
       
        if(i_get.sprite.name.Contains("Raspberry-5"))
        {
          pol.sprite=pol_s[0];
          rr=r;
          spash.sprite=spash_s[rr];
          
          i_set.sprite=random_bery1[r];
        }
        if(i_get.sprite.name.Contains("Blueberry-3"))
        {
          pol.sprite=pol_s[1];
          rr=r+4;
          spash.sprite=spash_s[rr];
          
          i_set.sprite=random_bery2[r];
        }
        
        if(i_get.sprite.name.Contains("strawberry-1"))
        {
          pol.sprite=pol_s[2];
          rr=r+8;
          spash.sprite=spash_s[rr];
          
          i_set.sprite=random_bery3[r];
        }
        if(i_get.sprite.name.Contains("Gooseberry-4-1"))
        {
          pol.sprite=pol_s[3];
          rr=r+12;
          spash.sprite=spash_s[rr];
          i_set.sprite=random_bery4[r];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
