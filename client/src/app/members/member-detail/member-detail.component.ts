import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { MembersService } from 'src/app/_services/members.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from '@kolkov/ngx-gallery';
//import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';
import { PostService } from 'src/app/_services/post.service';
import { PresenceService } from 'src/app/_services/presence.service';
import { AccountService } from 'src/app/_services/account.service';
import { User } from 'src/app/_models/user';
import { take } from 'rxjs/operators';
import { Post } from 'src/app/_models/post';
import { Pagination } from 'src/app/_models/pagination';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit, OnDestroy {
  //@ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  member: Member;
  //activeTab: TabDirective;
  messages: Message[] = [];
  //posts: Post[] = [];
  //pagination: Pagination;
  user: User;

  /*constructor(private memberService: MembersService, private route: ActivatedRoute, 
    private messageService: MessageService) { }*/
    
  constructor(public presence: PresenceService, 
              private route: ActivatedRoute, 
              private messageService: MessageService, 
              private postService: PostService, 
              private accountService: AccountService,
              private router: Router) { 
      this.accountService.currentUser$.pipe(take(1)).subscribe(user => this.user = user);
      this.router.routeReuseStrategy.shouldReuseRoute = () => false;
    }

  ngOnInit(): void {
    this.route.data.subscribe(data => {
      this.member = data.member;
    })
    /*
    this.route.queryParams.subscribe(params => {
      params.tab ? this.selectTab(params.tab) : this.selectTab(3);
    })
    */
    this.loadMessages();
    //this.loadPosts();
    this.messageService.createHubConnection(this.user, this.member.username);

  }

  loadMessages() {
    this.messageService.getMessageThread(this.member.username).subscribe(messages => {
      this.messages = messages;
    })
  }
  /*
  selectTab(tabId: number) {
    this.memberTabs.tabs[tabId].active = true;
  }
  
  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages'){ 
      //this.loadMessages();
      if(this.messages.length === 0) {
        this.messageService.createHubConnection(this.user, this.member.username);
      } 
    } else {
      //this.messageService.stopHubConnection();
    }
  }
  */
  ngOnDestroy(): void {
    this.messageService.stopHubConnection();
  }

}
