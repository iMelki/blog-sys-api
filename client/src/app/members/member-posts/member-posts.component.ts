import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Pagination } from 'src/app/_models/pagination';
import { Post } from 'src/app/_models/post';
import { UserParams } from 'src/app/_models/userParams';
import { MembersService } from 'src/app/_services/members.service';
import { PostService } from 'src/app/_services/post.service';

@Component({
  selector: 'app-member-posts',
  templateUrl: './member-posts.component.html',
  styleUrls: ['./member-posts.component.css']
})
export class MemberPostsComponent implements OnInit {
  @ViewChild('messageForm') messageForm: NgForm;
  @Input() posts: Post[];
  pagination: Pagination;
  userParams: UserParams;
  @Input() username: string;

  constructor(public postService: PostService) { }

  ngOnInit(): void {
    this.loadPosts();
  }
  
  loadPosts() {
    this.postService.getPostsByUsername(this.username).subscribe(response => {
      this.posts = response.result;
      this.pagination = response.pagination;
    })
  }

}
